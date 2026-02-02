/**
 * @fileOverview dB schema for a settlement channel.
 */

import { default as mongoose } from 'mongoose';
import { CONSTANTS } from '../../../utils/imports';
import TransactionSchema from './transaction';

// Destructure enums.
const { clearing : { ChannelTypeEnum } } = CONSTANTS;

/**
 * dB schema for a settlement channel.
 */
const ChannelSchema = new mongoose.Schema({
    addressFrom: {
        type: String,
        required: true,
    },

    addressTo: {
        type: String,
        required: true,
    },

    amount: {
        type: String,
        required: true,
    },

    asset: {
        type: String,
        required: true,
    },

    channelID: {
        index: true,
        unique: true,
        type: String,
        required: true,
    },

    commission: {
        type: String,
        required: true,
    },

    hashedSecret: {
        type: String,
        required: false,
    },

    timeout: {
        type: Number,
        required: true,
    },

    txContract: {
        type: TransactionSchema,
        required: true,
    },

    txRedeem: {
        type: TransactionSchema,
        required: true,
    },

    txRefund: {
        type: TransactionSchema,
        required: true,
    },

    type: {
        type: String,
        required: true,
        enum: Object.keys(ChannelTypeEnum)
    },

    redeemFee: {
        type: Number,
        required: false, // only for UTXO based DLTs
    },

    refundFee: {
      type: Number,
      required: false, // only for UTXO based DLTs
  },
}, {
    collection: 'clearing-channel',
    timestamps: { createdAt: 'timestamp', updatedAt: 'timestampUpdate' }
});

/**
 * Virtual property: gets all transactions.
 */
ChannelSchema.virtual('transactions').get(function () {
    return [
        this.txContract,
        this.txRefund,
        this.txRedeem,
    ];
});

/**
 * Virtual property: gets all transactions.
 */
ChannelSchema.virtual('pendingTransactions').get(function () {
    return this.getTransactionsByState('PENDING');
});

/**
 * Virtual method: gets all transactions across all channels in a certain state.
 */
ChannelSchema.method('getTransactionsByState', function (state) {
    return this.transactions.filter((tx) => {
        return tx.state === state;
    })
});

export default ChannelSchema;
