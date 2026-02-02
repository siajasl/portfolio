import { Subject } from 'rxjs';

export class ListenerFilterEngine {
  /**
   * contains txs that are being listened for
   */
  listeningTxs: string[] = [];

  /**
   * RxJs subject that emits the txs that are being listened for whenever their state changes
   */
  listeningTxsSubject: Subject<string[]> = new Subject();

  /**
   * adds a transaction id to the filtered txs array
   */
  addTx = (txId: string) =>
    (this.listeningTxs = [...this.listeningTxs, txId]);
  
  /**
   * remove a transaction id from the filtered txs array
   */
  removeTx = (tx: string) => {
    const txIndex = this.listeningTxs.indexOf(tx);
    if (txIndex === -1) {
      return;
    }
    this.listeningTxs = this.listeningTxs.slice(0, txIndex)
      .concat(this.listeningTxs.slice(txIndex + 1))
  }

  /**
   * emits the current txs that are being listened for to all subscribers of 'listeningTxsSubject'
   */
  emitFilteredTransactions = () => this.listeningTxsSubject.next(this.listeningTxs)
}