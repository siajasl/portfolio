/**
 * @fileOverview DLT hook initialiser.
 */

import { DLT_NETWORK_TYPE } from '../utils/constants';
import { sleep } from '../utils/threading';
import { listenerFactory, ListenerType } from '../dlt/listener';
import onTxContract from './onTxContract';
import onTxRedeem from './onTxRedeem';
import onTxRefund from './onTxRefund';
import {
    iwTxServices,
    listenToTx,
    recheckAllFilteredTxsIfMined
} from './index';


// Map of listener types to tx callback handlers.
const HANDLERS = {}
HANDLERS[ListenerType.Initiate] = onTxContract;
HANDLERS[ListenerType.Participate] = onTxContract;
HANDLERS[ListenerType.Redeem] = onTxRedeem;
HANDLERS[ListenerType.Refund] = onTxRefund;


/**
 * Initialises distributed ledger hooks.
 * @param {Object} dbe - Database engine wrapper.
 * @param {Object} pubsub - Web-socket event pubsub manager.
 */
export default async (dbe, pubsub) => {
    // Pull supported asset types from dB.
    const assetTypes = await dbe.getAssets();

    // Connect to iw tx services.
    for (const { symbol: assetType } of assetTypes) {
        await iwTxServices[assetType].connect();
    }

    // Instantiate listeners by asset & listener type.
    for (const { symbol: assetType } of assetTypes) {
        for (const listenerType of Object.keys(ListenerType)) {
            await setListener(dbe, pubsub, assetType, listenerType);
        }
    }

    // Relisten to pending transactions.
    const pendingTransactions = await getPendingTransactions(dbe);
    for (const [asset, channelType, txType, txHash] of pendingTransactions) {
        await listenToTx(asset, channelType, txType, txHash);
    }

    // Ensured already mined transactions are processed.
    await verifyIfMined(pendingTransactions);
}

/**
 * Returns all pending transactions.
 */
const getPendingTransactions = async (dbe) => {
    let result = [];
    const settlements = await dbe.getSettlementWithPendingTxList();
    for (const settlement of settlements) {
        for (const channel of settlement.channels) {
            for (const tx of channel.pendingTransactions) {
                result.push([channel.asset, channel.type, tx.type, tx.hash]);
            }
        }
    }

    return result.sort();
}

/**
 * Instantiate a listener.
 */
const setListener = async (dbe, pubsub, assetType, listenerType) => {
    // Instantiate.
    const listener = listenerFactory.getInstance(assetType, listenerType);

    // Configure.
    listener.setNetwork(DLT_NETWORK_TYPE);

    // Route.
    const onTxCallback = async (txDetails) => {
        const handler = HANDLERS[listenerType];
        await handler(dbe, pubsub, assetType, txDetails);
    }

    // Set callback.
    if (assetType === 'BTC') {
        listener.listen().subscribe(txPromise => {
            txPromise.then(async txDetails => await onTxCallback(txDetails));
        });
    } else {
        listener.listen().subscribe(onTxCallback);
    }
}

/**
 * Verify if any pending transactions have already been mined.
 */
const verifyIfMined = async (pendingTransactions) => {
    // Sleep to avoid race conditions.
    const tenSeconds = 10 * 1000;
    await sleep(tenSeconds);

    // Reduce to tuple: 1 tx per (asset, listenerType).
    const pendingTransactionsUnique = pendingTransactions.reduce((txs, tx) =>
        txs.find(t =>
            t[0] === tx[0] && // asset (e.g. 'BTC')
            t[1] === tx[1] && // channel type (e.g. 'INITIATE')
            t[2] === tx[2], // tx type (e.g. 'CONTRACT')
        )
            ? txs
            : [...txs, tx],
        [],
    );

    // Recheck for mined transactions.
    for (const [asset, channelType, txType, txHash] of pendingTransactionsUnique) {
        await recheckAllFilteredTxsIfMined(asset, channelType, txType);
    }
}
