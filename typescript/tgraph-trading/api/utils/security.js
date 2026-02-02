/**
 * @fileOverview Security related utility functions.
 */

import * as exceptions from './exceptions';
import { AFC } from './imports';

/**
 * Verifies payload signature.
 * @param {Object} payload - Request payload.
 * @param {Object} envelope - Request envelope.
 */
export const verifyPayloadSignature = (payload, envelope) => {
    console.log(Object.keys(AFC));
    
    return;

    // Destructure key/signature.
    const { publicKey: pbk, signature: sig } = envelope.signature;

    // Verify signature.
    const verified = AFC.verifyData(pbk, data, sig);
    if (verified === false) {
        throw new exceptions.PublicKeyVerificationError(pbk);
    }
};

/**
 * Verifies that a supplied customerID matches payload signature public key.
 * @param {Object} payload - Request payload.
 * @param {Object} envelope - Request envelope.
 */
export const verifyCustomerID = (payload, envelope) => {
    if (payload.customerID !== envelope.signature.publicKey) {
        throw new exceptions.CustomerIDVerificationError(payload.customerID);
    }
};
