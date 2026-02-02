/**
 * @fileOverview dB schema for an asset traded upon trading engine.
 */

import { default as mongoose } from 'mongoose';

/**
 * dB schema for an asset.
 */
const AssetSchema = new mongoose.Schema({
    symbol: {
        index: true,
        unique: true,
        type: String,
        required: true,
    },

    decimals: {
        type: Number,
        required: true,
        min: 0,
        max: 31,
    },
}, {
    collection: 'trading-asset',
    timestamps: { createdAt: 'timestamp', updatedAt: 'timestampUpdate' }
});

export default AssetSchema;
