/**
 * @fileOverview dB schema for an entity associated with a corporate entity.
 */

import {default as mongoose} from 'mongoose';
import * as constants from './constants';
import DocumentSchema from './document';

/**
 * dB schema for an entity associated with a corporate entity.
 */
export default new mongoose.Schema({
    associationType: {
        type: String,
        required: true,
    },

    certificateOfIncorporation: {
        type: DocumentSchema,
        required: false,
    },

    contactDetails: {
        type: Object,
        required: true,
    },

    isPoliticallyExposed: {
        type: Boolean,
        required: false,
    },

    name: {
        type: String,
        required: true,
    },

    registrationNumber: {
        type: String,
        required: true,
    },

    sourceOfFunds: {
        type: DocumentSchema,
        required: false,
    },

    sourceOfWealth: {
        type: DocumentSchema,
        required: false,
    },
}, {
    collection: 'kyc-associated-entity',
    timestamps: true
});
