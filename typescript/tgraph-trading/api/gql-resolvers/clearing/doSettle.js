/**
 * @fileOverview Processes initiate channel setup.
 */

import { CONSTANTS } from '../../utils/imports';
import compose from '../shared/resolvers/factory';
import * as dltHooks from '../../dlt-hooks/index';
import onSettlementStateChange from '../clearing/onSettlementStateChange';

// Destructure enums.
const { clearing : {
    ChannelTypeEnum,
    SettlementStateEnum,
    TransactionStateEnum,
    TransactionTypeEnum
} } = CONSTANTS;

/**
 * Endpoint resolver.
 */
const resolve = async (parent, { input, envelope }, { clearing, dbe, pubsub }, info) => {
    // Destructure.
    const {
        channelID,
        customerID,
        hashedSecret,
        settlementID,
        txContractHash
    } = input;

    // Pull.
    const settlement = await dbe.getSettlement({ customerID, settlementID });
    const channel = settlement.getChannel(channelID);

    // Validate.
    validate(settlement, channel, customerID);

    // Process.
    switch (channel.type) {
        case ChannelTypeEnum.INITIATE:
            await setChannelOne(dbe, settlement, hashedSecret, txContractHash);
            break;
        case ChannelTypeEnum.PARTICIPATE:
            await setChannelTwo(dbe, settlement, hashedSecret, txContractHash);
            break;
    }

    // Publish events.
    await publishEvents(settlement, pubsub);

    return true;
};

/**
 * Validate resolver inputs.
 */
const validate = (settlement, channel, customerID) => {
    if (settlement === null) {
        throw new Error('settlement not found');
    }

    if (channel === null) {
        throw new Error('channel not found');
    }

    if (channel.type === ChannelTypeEnum.INITIATE) {
        if (settlement.counterPartyOneID !== customerID) {
            throw new Error('settlement initiation can only be executed by counter party one');
        }
        if (settlement.state !== SettlementStateEnum.InitiateSetupAwaiting) {
            throw new Error('settlement is not awaiting initiation ');
        }
    }

    if (channel.type === ChannelTypeEnum.PARTICIPATE) {
        if (settlement.counterPartyTwoID !== customerID) {
            throw new Error('settlement participation can only be executed by counter party two');
        }
        if (settlement.state !== SettlementStateEnum.ParticipateSetupAwaiting) {
            throw new Error('settlement is not awaiting participation ');
        }
    }
};

/**
 * Establishes settlement channel 1.
 */
const setChannelOne = async (dbe, settlement, hashedSecret, txContractHash) => {
    // Update state.
    settlement.state = SettlementStateEnum.InitiateSetupDone;

    // Persist.
    await dbe.updateSettlement({ settlementID: settlement.settlementID }, {
        $push: {
            stateHistory: {
                state: settlement.state
            },
            "initiateChannel.txContract.stateHistory": {
                state: TransactionStateEnum.PENDING
            }
        },
        $set: {
            state: settlement.state,
            "initiateChannel.hashedSecret": hashedSecret,
            "initiateChannel.txContract.hash": txContractHash,
            "initiateChannel.txContract.state": TransactionStateEnum.PENDING,
        }
    });

    // Listen.
    await dltHooks.listenToTx(
        settlement.initiateChannel.asset,
        ChannelTypeEnum.INITIATE,
        TransactionTypeEnum.CONTRACT,
        txContractHash,
    );
}

/**
 * Establishes settlement channel 2.
 */
const setChannelTwo = async (dbe, settlement, hashedSecret, txContractHash) => {
    // Update state.
    settlement.state = SettlementStateEnum.ParticipateSetupDone;

    // Persist.
    await dbe.updateSettlement({ settlementID: settlement.settlementID }, {
        $push: {
            stateHistory: {
                state: settlement.state
            },
            "participateChannel.txContract.stateHistory": {
                state: TransactionStateEnum.PENDING
            }
        },
        $set: {
            state: settlement.state,
            "participateChannel.hashedSecret": hashedSecret,
            "participateChannel.txContract.hash": txContractHash,
            "participateChannel.txContract.state": TransactionStateEnum.PENDING,
        }
    });

    // Listen.
    await dltHooks.listenToTx(
        settlement.participateChannel.asset,
        ChannelTypeEnum.PARTICIPATE,
        TransactionTypeEnum.CONTRACT,
        txContractHash,
    );
}

/**
 * Publishes events.
 */
const publishEvents = async (settlement, pubsub) => {
    // Destructure.
    const { settlementID, state, counterParties } = settlement;

    // Publish event: onSettlementStateChange.
    for (const customerID of counterParties) {
        await onSettlementStateChange.publish(pubsub, {
            customerID,
            settlementID,
            state
        });
    }
}

// Export composed resolver.
export default compose(resolve, 'establishing settlement channel');
