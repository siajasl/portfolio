/**
 * @fileOverview dB schema for a corporate customer.
 */

// Module imports.
import {default as mongoose} from 'mongoose';
import * as constants from './constants';
import AssociatedEntitySchema from './associatedEntity';
import AssociatedIndividualSchema from './associatedIndividual';
import DocumentSchema from './document';

/**
 * dB schema for an individual customer.
 */
export default new mongoose.Schema({
    associatedEntities: {
        type: [AssociatedEntitySchema],
        required: false,
    },

    associatedIndividuals: {
        type: [AssociatedIndividualSchema],
        required: false,
    },

    contactDetails: {
        type: Object,
        required: true,
    },

    customerID: {
        index: true,
        unique: true,
        type: String,
        required: true,
    },

    documents: {
        type: [DocumentSchema],
        required: true,
    },

    expectedYearlyInvestment: {
        type: Number,
        required: false,
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

    verificationLevel: {
        type: String,
        required: true,
        enum: constants.VERIFICATION_LEVEL,
    },

    verificationStatus: {
        type: String,
        required: true,
        enum: constants.VERIFICATION_STATE,
    },
}, {
    collection: 'kyc-corporate-customer',
    timestamps: true
});
