/**
 * @fileOverview dB schema for an identity card.
 */

// Module imports.
import {default as mongoose} from 'mongoose';
import * as constants from './constants';
import DocumentSchema from './document';

/**
 * dB schema for an identity card.
 */
export default new mongoose.Schema({
    cardNumber: {
        type: String,
        required: true,
    },

    cardType: {
        type: String,
        required: true,
        enum: constants.IDENTITY_CARD,
    },

    expiryDate: {
        type: Date,
        required: false,
    },

    scanEncoding: {
        type: String,
        required: true,
    },

    scanOfFrontis: {
        type: String,
        required: true,
    },

    scanOfReverse: {
        type: String,
        required: false,
    },

    scanOfSelfie: {
        type: String,
        required: true,
    }
}, {
    collection: 'kyc-identity-card-scan',
    timestamps: true
});
