"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const accessfile_tx_1 = require("@trinkler/accessfile-tx");
const rxjs_1 = require("rxjs");
const operators_1 = require("rxjs/operators");
const _1 = require(".");
const is_mined_1 = require("../is-mined");
const slidingWindowSize = 10;
class EthTxListener extends _1.TxListenerBase {
    constructor() {
        super(...arguments);
        this.connect = async (network = accessfile_tx_1.Network.Prod) => accessfile_tx_1.ethWalletService.connect(network);
        this.recheckIfMined = async () => {
            const minedTxs = await this.filterEngine.listeningTxs.reduce(async (minedTxs, txId) => {
                const minedTx = await is_mined_1.isEthTxMined(txId);
                return Promise.resolve(minedTx
                    ? [...await minedTxs, minedTx]
                    : minedTxs);
            }, Promise.resolve([]));
            this.minedTxSubject.next(minedTxs);
        };
        /**
         * RxJs subject that emits a tx that was mined
         */
        this.minedTxSubject = new rxjs_1.Subject(); // TODO: change type to MinedTransaction, as soon as supported
        // https://github.com/ethereum/web3.js/issues/1965
        this.listen = () => rxjs_1.combineLatest(rxjs_1.merge(accessfile_tx_1.ethWalletService
            .watchAllMinedTransactions()
            .pipe(
        // accumulates/caches transactions over time (similar to reduce), so we can filter after them later
        operators_1.scan((accumulatedTxs, currentTxs) => [
            ...accumulatedTxs.filter(tx => currentTxs[0] && tx.blockNumber > currentTxs[0].blockNumber - slidingWindowSize),
            ...currentTxs
        ], [])), this.minedTxSubject), this.filterEngine.listeningTxsSubject).pipe(
        // filter for matched transactions
        operators_1.map(([allTxs, filteredId]) => allTxs.filter(tx => filteredId.includes(tx.id))), 
        // if no transactions matched, txs will be empty
        // hence, it has to be filtered out
        operators_1.filter(txs => txs.length > 0), 
        // remove matched transaction(s) from filtered transactions in filter engine
        operators_1.tap(txs => txs.forEach(tx => this.removeTx(tx.id))), 
        // emit transactions seperately (without 'merging all' it would emit arrays of transactions)
        operators_1.mergeAll());
        this.setNetwork = (network) => accessfile_tx_1.ethWalletService.setNetwork(network);
    }
}
exports.EthTxListener = EthTxListener;
//# sourceMappingURL=eth-tx-listener.js.map