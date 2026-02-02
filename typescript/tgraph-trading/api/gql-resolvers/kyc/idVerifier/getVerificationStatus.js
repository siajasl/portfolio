/**
 * @fileOverview Invokes the verifier's initiate verification API endpoint.
 */

import request from 'request-promise-native';
import * as constants from './constants';
import getData from './getData';
import getProcessingStatus from './getProcessingStatus';

/**
 * Returns verification status.
 * @param {string} verificationTxReference - Verifiers's transaction identifier.
 * @param {string} lastName - Customer's last name.
 * @param {string} dateOfBirth - Customer's date of birth.
 * @return {String} Pending | Rejected | Verified.
 */
export default async (verificationTxReference, lastName, dateOfBirth) => {
    // Error if tx reference is unknown.
    const processingState = await getProcessingStatus(verificationTxReference);
    if (processingState === 'UNKNOWN-TX') {
        throw new Error(`Unknown verifier transaction reference: ${verificationTxReference}`);
    }

    // Pending if still processing.
    if (processingState !== 'DONE') {
        return { status: 'Pending' };
    }

    // Get verification data from verifier.
    const data = await getData(verificationTxReference);

    // Pending if data is unavailable - should never happen.
    if (data === null) {
        return { status: 'Pending' };
    }

    // Rejected if verification data is not approved & verified.
    if (data.document.status !== 'APPROVED_VERIFIED') {
        return { status: 'Rejected', rejectionReason: 'Rejected by ID verifier' };
    }

    // Rejected if identity document is invalid.
    if (data.verification.identityVerification.validity !== 'true') {
        return { status: 'Rejected', rejectionReason: 'Invalid identity document' };
    }

    // Rejected if mismatched last name.
    if (data.document.lastName.toLowerCase() !== lastName.toLowerCase()) {
        return { status: 'Rejected', rejectionReason: 'Last name mismatch' };
    }

    // Rejected if mismatched date of birth.
    // if (data.document.dob !== dateOfBirth) {
    //     return { status: 'Rejected', rejectionReason: 'Date of birth mismatch' };
    // }

    // Verified as all rules passed.
    return { status: 'Verified' };
}
