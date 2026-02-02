/**
 * @fileOverview Graphql utility functions.
 */

import * as _ from 'lodash';
import * as uuid from 'uuid';
import { mapString } from './mappers';
import * as crypto from '../crypto';
import * as logger from '../logging';
import { Options as options } from '../options';

// Endpoints not requiring customer AF signature.
const OPEN_ENDPOINTS = new Set(['getBook', 'getDailyVolume']);

/**
 * Returns contextual information dispatched with each request.
 * @param {object} data - Data to be dispatached to API.
 * @param {string} path - GQL endpoint path, e.g. kyc.getIndividualCustomer.
 * @return {object} GQL string consisting of the ECC sig and public key.
 */
export default (data, path) => {
    const isOpen = OPEN_ENDPOINTS.has(path);
    if (isOpen) {
        return `
        {
            correlationID: ${mapString(options.CORRELATION_ID)}
            nonce:         ${mapString(uuid.v4())}
            timestamp:     ${mapString(Date.now())}
        }
        `;
    } else {
        const { publicKey, signature } = crypto.getRequestSignature(options.AF_KEY_PAIR, data);
        return `
        {
            correlationID: ${mapString(options.CORRELATION_ID)}
            nonce:         ${mapString(uuid.v4())}
            signature: {
                publicKey: ${mapString(publicKey)}
                signature: ${mapString(signature)}
            },
            timestamp:     ${mapString(Date.now())}
        }
        `;
    }
};
