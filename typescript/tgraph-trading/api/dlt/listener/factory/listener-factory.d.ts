import { AssetClass } from '@trinkler/accessfile-tx';
import { ListenerType } from '.';
import { DltTxListener } from '../dlt-tx-listener';
declare class ListenerFactory {
    private listeners;
    getInstance: (assetClass: AssetClass, listenerType: ListenerType) => DltTxListener | null;
    private getExistingInstance;
    private getNewInstance;
    private addInstance;
}
export declare const listenerFactory: ListenerFactory;
export {};
