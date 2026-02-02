/**
 * @fileOverview DLT hooks.
 */

import {
    btcWalletService as walletServiceBTC,
    ethWalletService as walletServiceETH
} from '@trinkler/accessfile-tx';
import * as logger from '../utils/logging';
import { listenerFactory } from '../dlt/listener';

// Map: asset type <-> iw tx service.
const iwTxServices = {
    'BTC': walletServiceBTC,
    'ETH': walletServiceETH
}

// Map: channel.transaction type <-> listener type.
const mapOfChannelAndTxTypeToListenerType = {
    'INITIATE.CONTRACT': 'Initiate',
    'INITIATE.REDEEM': 'Redeem',
    'INITIATE.REFUND': 'Refund',
    'PARTICIPATE.CONTRACT': 'Participate',
    'PARTICIPATE.REDEEM': 'Redeem',
    'PARTICIPATE.REFUND': 'Refund',
}

/**
 * Gets a listener instance
 */
const getListenerInstance = (assetType, channelType, transactionType) => {
  const listenerTypeKey = `${channelType}.${transactionType}`;
  const listenerType = mapOfChannelAndTxTypeToListenerType[listenerTypeKey];

  return listenerFactory.getInstance(assetType, listenerType);
}

/**
 * Adds a transaction to a listener.
 */
const listenToTx = async (assetType, channelType, transactionType, txHash) => {
    const listener = getListenerInstance(assetType, channelType, transactionType);

    listener.addTx(txHash);

    logger.logDLT(`listening tx: ${assetType} :: ${txHash}`);
}

/**
 * Rechecks all txs being listened whether they've been mined
 * (useful to check after restarting the API)
 */
const recheckAllFilteredTxsIfMined = async (assetType, channelType, transactionType) => {
    logger.logDLT(`rechecking if any tx was mined during downtime: ${assetType} :: ${channelType}.${transactionType}`);

    const listener = getListenerInstance(assetType, channelType, transactionType);

    return listener.recheckIfMined();
}

export {
    iwTxServices,
    listenToTx,
    recheckAllFilteredTxsIfMined
}
