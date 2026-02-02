/**
 * @fileOverview Returns proof of identity information pulled from verifier.
 */

import * as constants from './constants';
import getData from './getData';
import getScan from './getScan';

/**
 * Returns proof of identity information.
 * @param {string} verificationTxReference - Verifiers's transaction identifier.
 * @return {Object} Identity proof.
 */
export default async (verificationTxReference) => {
    // Pull data from identity verifer.
    const scanOfFrontis = await getScan(verificationTxReference, 'id-frontis');
    const scanOfSelfie = await getScan(verificationTxReference, 'selfie');
    const data = await getData(verificationTxReference);
    if (data === null || scanOfFrontis === null || scanOfSelfie === null) {
        throw new Error('Verification transaction is either invalid or expired');
    }

    return {
        scanOfFrontis,
        scanOfSelfie,
        cardType: data.document.type,
        cardNumber: data.document.number,
        expiryDate: data.document.expiry,
        scanEncoding: 'base64',
    };
}
