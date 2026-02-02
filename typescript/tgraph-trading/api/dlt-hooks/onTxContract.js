/**
 * @fileOverview DLT hooks for contract deployment related events.
 */

import { ClearingEngine } from '../utils/imports';
import onSettlementParticipate from '../gql-resolvers/clearing/onSettlementParticipate';
import onSettlementStateChange from '../gql-resolvers/clearing/onSettlementStateChange';

const { SettlementStateEnum, TransactionStateEnum } = ClearingEngine;

/**
 * DLT event callback invoked upon processing of a channel contract tx upon a supported DLT.
 * @param {String} assetType - Type of asset being settled, e.g. ETH.
 * @param {Object} txDetails - Transaction details such as hash, status ...etc.
 */
export default async (dbe, pubsub, assetType, txDetails) => {
    // Destructure tx details emitted from dlt listener.
    const {
      txid, // in case of 'BTC'
      txHash // in case of 'ETH'
    } = txDetails;
    const txId = txid || txHash;

    // Pull.
    const settlement = await dbe.getSettlementByTxHash(txId);
    const tx = settlement.getTransactionByHash(txId);

    // Update.
    settlement.state = tx === settlement.initiateChannel.txContract ?
                       SettlementStateEnum.ParticipateSetupAwaiting :
                       SettlementStateEnum.InitiateRedeemAwaiting;
    tx.state = TransactionStateEnum.CONFIRMED;

    // Persist.
    await persistUpdates(dbe, settlement, tx);

    // Publish events.
    await publishEvents(pubsub, settlement);
};

/**
 * Persists settlement updates to dB.
 */
const persistUpdates = async (dbe, settlement, tx) => {
    const { settlementID } = settlement;
    if (tx === settlement.initiateChannel.txContract) {
        await dbe.updateSettlement({ settlementID }, {
            $push: {
                stateHistory: {
                    state: settlement.state
                },
                "initiateChannel.txContract.stateHistory": {
                    state: tx.state
                }
            },
            $set: {
                state: settlement.state,
                "initiateChannel.txContract.state": tx.state,
            }
        });
    } else {
        await dbe.updateSettlement({ settlementID }, {
            $push: {
                stateHistory: {
                    state: settlement.state
                },
                "participateChannel.txContract.stateHistory": {
                    state: tx.state
                }
            },
            $set: {
                state: settlement.state,
                "participateChannel.txContract.state": tx.state,
            }
        });
    }
}

/**
 * Publishes events over web-socket channel.
 */
const publishEvents = async (pubsub, {
    counterPartyOneID,
    counterPartyTwoID,
    settlementID,
    state
}) => {
    // Publish event: onSettlementStateChange.
    for (const customerID of [counterPartyOneID, counterPartyTwoID]) {
        await onSettlementStateChange.publish(pubsub, {
            customerID,
            settlementID,
            state
        });
    }

    // Publish event: onSettlementParticipate (cp2 only).
    await onSettlementParticipate.publish(pubsub, {
        settlementID,
        customerID: counterPartyTwoID
    });
}
