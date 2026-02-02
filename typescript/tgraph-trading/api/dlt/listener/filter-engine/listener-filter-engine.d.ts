import { Subject } from 'rxjs';
export declare class ListenerFilterEngine {
    /**
     * contains txs that are being listened for
     */
    listeningTxs: string[];
    /**
     * RxJs subject that emits the txs that are being listened for whenever their state changes
     */
    listeningTxsSubject: Subject<string[]>;
    /**
     * adds a transaction id to the filtered txs array
     */
    addTx: (txId: string) => string[];
    /**
     * remove a transaction id from the filtered txs array
     */
    removeTx: (tx: string) => void;
    /**
     * emits the current txs that are being listened for to all subscribers of 'listeningTxsSubject'
     */
    emitFilteredTransactions: () => void;
}
