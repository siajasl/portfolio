"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const accessfile_tx_1 = require("@trinkler/accessfile-tx");
const rxjs_1 = require("rxjs");
const operators_1 = require("rxjs/operators");
const _1 = require(".");
const slidingWindowSize = 30;
// "the figure 7tx/s is commonly quoted as a 'ball-park' approximation in discussions of the blocksize limit."
// source: https://en.bitcoin.it/wiki/Maximum_transaction_rate
const ballParkBlockSizeLimit = 7;
const blockTime = 10 * 60; // in seconds
const maxTxPerBlock = ballParkBlockSizeLimit * blockTime;
const slidingWindowTxs = slidingWindowSize * maxTxPerBlock;
class BtcTxListener extends _1.TxListenerBase {
    constructor() {
        super(...arguments);
        this.connect = async (network = accessfile_tx_1.Network.Prod) => accessfile_tx_1.btcWalletService.connect(network);
        this.recheckIfMined = async () => {
            const minedTxIds = await this.filterEngine.listeningTxs.reduce(async (minedTxIds, txId) => {
                const minedTx = await accessfile_tx_1.btcWalletService.getTransaction(txId);
                return Promise.resolve(minedTx
                    ? [...await minedTxIds, txId]
                    : minedTxIds);
            }, Promise.resolve([]));
            if (minedTxIds.length > 0) {
                this.minedTxSubject.next(minedTxIds);
            }
        };
        /**
         * RxJs subject that emits a tx that was mined
         */
        this.minedTxSubject = new rxjs_1.Subject(); // TODO: change type to MinedTransaction, as soon as supported
        this.listen = () => rxjs_1.combineLatest(rxjs_1.merge(accessfile_tx_1.btcWalletService.watchAllMinedTransactionHashes()
            .pipe(
        // accumulates/caches transaction ids over time (similar to reduce), so we can filter after them later
        operators_1.scan((accumulatedTxIds, currentTxIds) => [
            // unlike in ETH, we don't know in which block a tx was mined
            // therefore, we conservatively estimate how many tx can be in 1 block
            // to assure, that the cached txs satisfies the sliding window requirements
            ...(accumulatedTxIds.length > slidingWindowTxs
                ? accumulatedTxIds.slice(0, slidingWindowTxs - 1)
                : accumulatedTxIds),
            currentTxIds
        ], [])), this.minedTxSubject), this.filterEngine.listeningTxsSubject)
            .pipe(
        // filter for matched transactions
        operators_1.map(([allTxIds, filteredTxIds]) => allTxIds.filter(txId => filteredTxIds.includes(txId))), 
        // if no transactions matched, txs will be empty
        // hence, it has to be filtered out
        operators_1.filter(txs => txs.length > 0), 
        // remove matched transaction(s) from filtered transactions in filter engine
        operators_1.tap(txIds => txIds.forEach(txId => this.removeTx(txId))), 
        // get full txs from each tx id
        operators_1.map(txIds => txIds.map(txId => accessfile_tx_1.btcWalletService.getTransaction(txId))), 
        // emit transactions seperately (without 'merging all' it would emit arrays of transactions)
        operators_1.mergeAll());
        this.setNetwork = (network) => accessfile_tx_1.btcWalletService.setNetwork(network);
    }
}
exports.BtcTxListener = BtcTxListener;
//# sourceMappingURL=btc-tx-listener.js.map