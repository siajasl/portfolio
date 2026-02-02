import { btcWalletService, Network } from '@trinkler/accessfile-tx';
import { combineLatest, Subject, merge } from 'rxjs';
import { filter, map, mergeAll, scan, tap } from 'rxjs/operators';

import { DltTxListener, TxListenerBase } from '.';

const slidingWindowSize = 30;

// "the figure 7tx/s is commonly quoted as a 'ball-park' approximation in discussions of the blocksize limit."
// source: https://en.bitcoin.it/wiki/Maximum_transaction_rate
const ballParkBlockSizeLimit = 7;
const blockTime = 10 * 60; // in seconds
const maxTxPerBlock = ballParkBlockSizeLimit * blockTime;
const slidingWindowTxs = slidingWindowSize * maxTxPerBlock;

export class BtcTxListener extends TxListenerBase implements DltTxListener {
  
  connect = async (network: Network = Network.Prod) =>
    btcWalletService.connect(network);

  recheckIfMined = async () => {
    const minedTxIds = await this.filterEngine.listeningTxs.reduce(async (minedTxIds: Promise<string[]>, txId: string) => {
      const minedTx = await btcWalletService.getTransaction(txId);

      return Promise.resolve(minedTx
        ? [ ...await minedTxIds, txId ]
        : minedTxIds);
    }, Promise.resolve([]));

    if (minedTxIds.length > 0) {
      this.minedTxSubject.next(minedTxIds);
    }
  }

  /**
   * RxJs subject that emits a tx that was mined
   */
  private minedTxSubject: Subject<any> = new Subject(); // TODO: change type to MinedTransaction, as soon as supported

  listen = () => combineLatest(
    merge(
      btcWalletService.watchAllMinedTransactionHashes()
        .pipe(
          // accumulates/caches transaction ids over time (similar to reduce), so we can filter after them later
          scan((accumulatedTxIds: string[], currentTxIds: string) => [

            // unlike in ETH, we don't know in which block a tx was mined
            // therefore, we conservatively estimate how many tx can be in 1 block
            // to assure, that the cached txs satisfies the sliding window requirements
            ...(accumulatedTxIds.length > slidingWindowTxs
              ? accumulatedTxIds.slice(0, slidingWindowTxs - 1)
              : accumulatedTxIds),
            currentTxIds
          ],
            []
          ),
        ),
      this.minedTxSubject,
    ),
    this.filterEngine.listeningTxsSubject,
  )
  .pipe(
    // filter for matched transactions
    map(([allTxIds, filteredTxIds]) =>
      allTxIds.filter(txId => filteredTxIds.includes(txId)),
    ),
    // if no transactions matched, txs will be empty
    // hence, it has to be filtered out
    filter(txs => txs.length > 0),
    // remove matched transaction(s) from filtered transactions in filter engine
    tap(txIds => txIds.forEach(txId => this.removeTx(txId))),

    // get full txs from each tx id
    map(txIds =>
      txIds.map(txId => btcWalletService.getTransaction(txId))
    ),
    // emit transactions seperately (without 'merging all' it would emit arrays of transactions)
    mergeAll(),
  )

  setNetwork = (network: Network) => btcWalletService.setNetwork(network);
}
