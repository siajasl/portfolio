/**
 * @fileOverview dB schema for a transaction boadcast to a DLT network.
 */

import { default as mongoose } from 'mongoose';
import { CONSTANTS } from '../../../utils/imports';
import { createStateHistorySchema } from '../utils';

// Destructure enums.
const { clearing : { TransactionStateEnum, TransactionTypeEnum } } = CONSTANTS;

/**
 * dB schema for a transaction state change.
 */
const TransactionStateSchema = createStateHistorySchema(TransactionStateEnum, 'clearing-transaction-state');

/**
 * dB schema for a transaction boadcast to a DLT network.
 */
const TransactionSchema = new mongoose.Schema({
    asset: {
        type: String,
        required: true,
    },

    hash: {
        type: String,
        required: false,
    },

    script: {
        type: String,
        required: false,
    },

    secret: {
        type: String,
        required: false,
    },

    signature: {
        type: String,
        required: false,
    },

    state: {
        type: String,
        required: true,
        enum: Object.keys(TransactionStateEnum)
    },

    stateHistory: {
        type: [TransactionStateSchema],
        required: false,
    },

    transactionID: {
        index: true,
        unique: true,
        type: String,
        required: true,
    },

    type: {
        type: String,
        required: true,
        enum: Object.keys(TransactionTypeEnum)
    },
}, {
    collection: 'clearing-transaction',
    timestamps: { createdAt: 'timestamp', updatedAt: 'timestampUpdate' }
});

export default TransactionSchema;
