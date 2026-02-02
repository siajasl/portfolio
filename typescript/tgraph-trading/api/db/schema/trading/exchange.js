/**
* @fileOverview dB schema for an exchange being handled by trading engine.
 */

import { default as mongoose } from 'mongoose';
import { CONSTANTS } from '../../../utils/imports';

// Destructure enums.
const { trading : { MatchingTypeEnum } } = CONSTANTS;

/**
 * dB schema for exchange htlc timeout settings.
 */
const ExchangeHtlcTimeoutsSchema = new mongoose.Schema({
    initiate: {
        type: Number,
        required: true
    },

    participate: {
        type: Number,
        required: true,
    },

    participateEffective: {
        type: Number,
        required: true,
    },
}, {
    _id: false,
    collection: 'trading-exchange-htlc-timeouts'
});

/**
 * dB schema for an exchange.
 */
const ExchangeSchema = new mongoose.Schema({
    assetPairs: {
        type: [String],
        required: true,
        uppercase: true,
        trim: true
    },

    htlcTimeouts: {
        type: ExchangeHtlcTimeoutsSchema,
        required: true,
    },

    commissionPercentage: {
        type: String,
        required: true,
    },

    exchangeID: {
        index: true,
        unique: true,
        type: String,
        required: true,
    },

    matchingAlgorithm: {
        type: String,
        required: true,
        enum: Object.keys(MatchingTypeEnum),
    },

    name: {
        unique: true,
        type: String,
        required: true,
    },

    symbol: {
        unique: true,
        type: String,
        required: true,
    }
}, {
    collection: 'trading-exchange',
    timestamps: { createdAt: 'timestamp', updatedAt: 'timestampUpdate' }
});

export default ExchangeSchema;
