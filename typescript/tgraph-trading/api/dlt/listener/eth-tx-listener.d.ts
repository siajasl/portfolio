import { Network } from '@trinkler/accessfile-tx';
import { DltTxListener, TxListenerBase } from '.';
export declare class EthTxListener extends TxListenerBase implements DltTxListener {
    connect: (network?: Network) => Promise<void>;
    recheckIfMined: () => Promise<void>;
    /**
     * RxJs subject that emits a tx that was mined
     */
    private minedTxSubject;
    listen: () => import("rxjs").Observable<unknown>;
    setNetwork: (network: Network) => void;
}
