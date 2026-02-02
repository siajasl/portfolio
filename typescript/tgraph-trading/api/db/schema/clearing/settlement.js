/**
 * @fileOverview dB schema for a trade matched by engine.
 */

import { default as mongoose } from 'mongoose';
import { CONSTANTS } from '../../../utils/imports';
import { createStateHistorySchema } from '../utils';
import ChannelSchema from './channel';

// Destructure enums.
const { clearing : { SettlementStateEnum, TransactionStateEnum } } = CONSTANTS;

/**
 * dB schema for a settlement state change.
 */
const SettlementStateSchema = createStateHistorySchema(SettlementStateEnum, 'clearing-settlement-state');

/**
 * dB schema for a clearing settlement.
 */
const SettlementSchema = new mongoose.Schema({
    assetPair: {
        type: String,
        required: true,
    },

    counterPartyOneID: {
        type: String,
        required: true,
    },

    counterPartyTwoID: {
        type: String,
        required: true,
    },

    initiateChannel: {
        type: ChannelSchema,
        required: true,
    },

    participateChannel: {
        type: ChannelSchema,
        required: true,
    },

    settlementID: {
        index: true,
        unique: true,
        type: String,
        required: true,
    },

    state: {
        type: String,
        required: true,
        enum: Object.keys(SettlementStateEnum)
    },

    stateHistory: {
        type: [SettlementStateSchema],
        required: false,
    },
}, {
    collection: 'clearing-settlement',
    timestamps: { createdAt: 'timestamp', updatedAt: 'timestampUpdate' }
});

/**
 * Virtual property: gets all channels.
 */
SettlementSchema.virtual('channels').get(function () {
    return [
        this.initiateChannel,
        this.participateChannel,
    ];
});

/**
 * Virtual property: gets all channels.
 */
SettlementSchema.virtual('counterParties').get(function () {
    return [
        this.counterPartyOneID,
        this.counterPartyTwoID,
    ];
});

/**
 * Virtual property: gets all transactions across all channels.
 */
SettlementSchema.virtual('transactions').get(function () {
    return [
        this.initiateChannel.txContract,
        this.initiateChannel.txRefund,
        this.initiateChannel.txRedeem,
        this.participateChannel.txContract,
        this.participateChannel.txRefund,
        this.participateChannel.txRedeem,
    ];
});

/**
 * Virtual method: gets a channel.
 */
SettlementSchema.method('getChannel', function (channelID) {
    return this.channels.find((c) => {
        return c.channelID === channelID;
    })
});

/**
 * Virtual method: gets all transactions across all channels in a certain state.
 */
SettlementSchema.method('getTransactionsByState', function (state) {
    return this.transactions.filter((tx) => {
        return tx.state === state;
    })
});

/**
 * Virtual method: gets first transaction with matching hash.
 */
SettlementSchema.method('getTransactionByHash', function (txHash) {
    return this.transactions.find((tx) => {
        return tx.hash === txHash;
    })
});

/**
 * Virtual method: gets all transactions across all channels where state = PENDING.
 */
SettlementSchema.method('getPendingTransactions', function () {
    return this.getTransactionsByState(TransactionStateEnum.PENDING);
});

export default SettlementSchema;
