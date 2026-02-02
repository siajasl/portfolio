/**
 * @fileOverview dB schema for a document.
 */

// Module imports.
import {default as mongoose} from 'mongoose';
import * as constants from './constants';

/**
 * dB schema for a document.
 */
export default new mongoose.Schema({
    content: {
        type: String,
        required: true,
    },

    documentType: {
        type: String,
        required: true,
    },

    encoding: {
        type: String,
        required: true,
        enum: constants.DOCUMENT_ENCODING
    },

    jurisdiction: {
        type: String,
        required: false,
    },

    validFromDate: {
        type: Date,
        required: false,
    },

    validToDate: {
        type: Date,
        required: false,
    },
}, {
    collection: 'kyc-document',
    timestamps: true
});
