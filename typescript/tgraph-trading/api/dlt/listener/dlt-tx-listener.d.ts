import { Network } from '@trinkler/accessfile-tx';
import { Observable } from 'rxjs/internal/Observable';
import { ListenerFilterEngine } from './filter-engine';
export interface DltTxListener {
    /**
     * the filter engine that maintains the state of transactions that should be filtered for
     */
    filterEngine: ListenerFilterEngine;
    /**
     * connect to the dlt network
     * (this method should be called first, before calling other methods)
     */
    connect: () => Promise<void>;
    /**
     * rechecks each filtered tx if it's been mined yet
     * (useful after restarting the API to check if any of the filtered txs has been mined during the downtime)
     */
    recheckIfMined: () => Promise<void>;
    /**
     * listen to mined transactions that are filtered for
     * (as defined in the filter engine)
     */
    listen: () => Observable<any>;
    /**
     * add an address which's incoming and outgoing transactions should be filtered for
     */
    addTx: (address: string) => any;
    /**
     * remove an address to stop filtering for its incoming and outgoing transactions
     */
    removeTx: (address: string) => any;
    /**
     * sets the network
     */
    setNetwork: (network: Network) => void;
}
export declare abstract class TxListenerBase {
    filterEngine: ListenerFilterEngine;
    addTx: (txId: string) => void;
    removeTx: (txId: string) => void;
}
