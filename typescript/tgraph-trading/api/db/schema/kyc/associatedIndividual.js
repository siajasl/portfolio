/**
 * @fileOverview dB schema for an individual associated with a corporate entity.
 */

 // Module imports.
import {default as mongoose} from 'mongoose';
import * as constants from './constants';
import DocumentSchema from './document';
import IdentityCardScanSchema from './identityCardScan';

/**
 * dB schema for an individual associated with a corporate entity.
 */
export default new mongoose.Schema({
    associationType: {
        type: String,
        required: true,
    },

    birthDetails: {
        type: Object,
        required: false,
    },

    capacity: {
        type: String,
        required: false,
    },

    contactDetails: {
        type: Object,
        required: false,
    },

    expectedYearlySalary: {
        type: Number,
        required: false,
    },

    isPoliticallyExposed: {
        type: Boolean,
        required: false,
    },

    name: {
        type: Object,
        required: true,
    },

    nationality: {
        type: String,
        required: true,
    },

    proofOfEmployment: {
        type: DocumentSchema,
        required: false,
    },

    proofOfIdentity: {
        type: IdentityCardScanSchema,
        required: false,
    },

    proofOfResidency: {
        type: [DocumentSchema],
        required: false,
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
    collection: 'kyc-associated-individual',
    timestamps: true
});
