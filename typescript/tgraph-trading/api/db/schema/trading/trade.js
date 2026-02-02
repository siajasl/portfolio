/**
 * @fileOverview dB schema for a trade matched by engine.
 */

import { default as mongoose } from 'mongoose';
import { CONSTANTS } from '../../../utils/imports';
import { createStateHistorySchema } from '../utils';

// Destructure enums.
const { trading : { TradeStateEnum } } = CONSTANTS;

/**
 * dB schema for a trade state change.
 */
const TradeStateSchema = createStateHistorySchema(TradeStateEnum, 'trading-trade-state');

/**
 * dB schema for a trade.
 */
const TradeSchema = new mongoose.Schema({
    assetPair: {
        type: String,
        required: true,
    },

    exchangeID: {
        type: String,
        required: true,
    },

    exchangeSymbol: {
        type: String,
        required: true,
    },

    makeCustomerID: {
        type: String,
        required: true,
    },

    makeOrderID: {
        type: String,
        required: true,
    },

    makeSide: {
        type: String,
        required: true,
    },

    price: {
        type: String,
        required: true,
    },

    quantity: {
        type: String,
        required: true,
    },

    settlementID: {
        type: String,
        required: true,
    },

    state: {
        type: String,
        required: true,
        enum: Object.keys(TradeStateEnum)
    },

    stateHistory: {
        type: [TradeStateSchema],
        required: false,
    },

    takeCustomerID: {
        type: String,
        required: true,
    },

    takeOrderID: {
        type: String,
        required: true,
    },

    takeSide: {
        type: String,
        required: true,
    },

    tradeID: {
        type: String,
        required: true,
    },
}, {
    collection: 'trading-trade',
    timestamps: { createdAt: 'timestamp', updatedAt: 'timestampUpdate' }
});

export default TradeSchema;
