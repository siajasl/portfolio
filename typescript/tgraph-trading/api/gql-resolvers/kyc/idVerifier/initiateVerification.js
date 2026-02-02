/**
 * @fileOverview Invokes the verifier's initiate verification API endpoint.
 */
import request from 'request-promise-native';
import * as constants from './constants';

/**
 * Invokes the verifier's initiate verification API endpoint.
 * @param {string} customerID - Customer's customer identifier.
 * @return {Object} URL to verifier's iframe & tx reference.
 */
export default async (customerID) => {
    // Set request payload.
    const payload = {
        customerInternalReference: customerID,
        userReference: customerID,
        tokenLifetimeInMinutes: constants.API_TOKEN_LIFETIME_IN_MINUTES,
    };

    // Invoke verifier's API.
    const response = await request({
        url: constants.URL_POST_INITIATE,
        body: JSON.stringify(payload),
        headers: constants.HTTP_HEADERS_JSON,
        method: 'POST',
        auth: constants.HTTP_BASIC_AUTHENTICATION,
    });
    const parsed = JSON.parse(response);

    return {
        iframeUrl: parsed.redirectUrl,
        verificationTxReference: parsed.transactionReference,
    };
}
