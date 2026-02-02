import { AssetClass } from '@trinkler/accessfile-tx';

import { ListenerType } from '.';
import { EthTxListener } from '..';
import { BtcTxListener } from '../btc-tx-listener';
import { DltTxListener } from '../dlt-tx-listener';

interface ListenerFactoryDefinition {
  assetClass: AssetClass,
  listenerType: ListenerType,
  listener: DltTxListener,
}

class ListenerFactory {
  private listeners: ListenerFactoryDefinition[] = [];

  getInstance = (assetClass: AssetClass, listenerType: ListenerType) =>
    this.getExistingInstance(assetClass, listenerType) ||
    this.getNewInstance(assetClass, listenerType)

  private getExistingInstance = (assetClass: AssetClass, listenerType: ListenerType) => {
    const listenerDefinition = this.listeners.find(l => l.assetClass === assetClass && l.listenerType === listenerType);

    if (!listenerDefinition) {
      return null;
    }

    return listenerDefinition.listener;
  }


  private getNewInstance = (assetClass: AssetClass, listenerType: ListenerType) => {
    if (this.getExistingInstance(assetClass, listenerType)) {
      return this.getExistingInstance(assetClass, listenerType);
    }

    switch (assetClass) {
      case AssetClass.BTC: {
        this.addInstance({
          assetClass: assetClass,
          listenerType: listenerType,
          listener: new BtcTxListener(),
        });
        break;
      }
      case AssetClass.ETH: {
        this.addInstance({
          assetClass: assetClass,
          listenerType: listenerType,
          listener: new EthTxListener(),
        });
        break;
      }
      default: {
        throw new Error(`AssetType ${assetClass} is not defined!`);
      }
    }

    return this.getExistingInstance(assetClass, listenerType);
  }

  private addInstance = (listener: ListenerFactoryDefinition) =>
    this.listeners = [
      ...this.listeners,
      listener,
    ]
}

export const listenerFactory = new ListenerFactory();
