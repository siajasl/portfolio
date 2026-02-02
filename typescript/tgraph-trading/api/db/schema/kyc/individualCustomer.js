/**
 * @fileOverview dB schema for an individual customer.
 */

// Module imports.
import {default as mongoose} from 'mongoose';
import * as constants from './constants';
import AssociatedEntitySchema from './associatedEntity';
import AssociatedIndividualSchema from './associatedIndividual';
import IdentityCardScanSchema from './identityCardScan';
import DocumentSchema from './document';

/**
 * dB schema for an individual customer.
 */
export default new mongoose.Schema({
    birthDetails: {
        type: Object,
        required: true,
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
    
    expectedYearlyInvestment: {
        type: Number,
        required: false,
    },

    expectedYearlySalary: {
        type: Number,
        required: false,
    },

    isActingAsAgent: {
        type: Boolean,
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

    principleEntity: {
        type: AssociatedEntitySchema,
        required: false,
    },

    principleIndividual: {
        type: AssociatedIndividualSchema,
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

    verificationTxReference: {
        type: String,
        required: false,
    },
}, {
    collection: 'kyc-individual-customer',
    timestamps: true
});
