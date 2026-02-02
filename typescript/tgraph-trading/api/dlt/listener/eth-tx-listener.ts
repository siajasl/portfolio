import { ethWalletService, Network, MinedTransaction } from '@trinkler/accessfile-tx';
import { combineLatest, Subject, merge } from 'rxjs';
import { filter, map, mergeAll, scan, tap } from 'rxjs/operators';

import { DltTxListener, TxListenerBase } from '.';
import { isEthTxMined } from '../is-mined';

const slidingWindowSize = 10;

export class EthTxListener extends TxListenerBase implements DltTxListener {

  connect = async (network: Network = Network.Prod) =>
    ethWalletService.connect(network);

  recheckIfMined = async () => {
    const minedTxs = await this.filterEngine.listeningTxs.reduce(async (minedTxs: Promise<MinedTransaction[]>, txId: string) => {
      const minedTx = await isEthTxMined(txId);
      
      return Promise.resolve(minedTx
        ? [...await minedTxs, minedTx]
        : minedTxs);
      }, Promise.resolve([]));

    this.minedTxSubject.next(minedTxs);
  }
      
  /**
   * RxJs subject that emits a tx that was mined
   */
  private minedTxSubject: Subject<any> = new Subject(); // TODO: change type to MinedTransaction, as soon as supported

  // https://github.com/ethereum/web3.js/issues/1965
  listen = () => combineLatest(
    merge(
      ethWalletService
        .watchAllMinedTransactions()
        .pipe(
  
          // accumulates/caches transactions over time (similar to reduce), so we can filter after them later
          scan((accumulatedTxs: any[], currentTxs: any) => [
              ...accumulatedTxs.filter(tx => currentTxs[0] && tx.blockNumber > currentTxs[0].blockNumber - slidingWindowSize),
              ...currentTxs
            ],
            []
          ),
        ),
      this.minedTxSubject,
    ),

    this.filterEngine.listeningTxsSubject
  ).pipe(

    // filter for matched transactions
    map(([allTxs, filteredId]) =>
      allTxs.filter(tx => filteredId.includes(tx.id))
    ),

    // if no transactions matched, txs will be empty
    // hence, it has to be filtered out
    filter(txs => txs.length > 0),

    // remove matched transaction(s) from filtered transactions in filter engine
    tap(txs => txs.forEach(tx => this.removeTx(tx.id))),

    // emit transactions seperately (without 'merging all' it would emit arrays of transactions)
    mergeAll(),
  );
  
  setNetwork = (network: Network) => ethWalletService.setNetwork(network);
}
