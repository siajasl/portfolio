/**
 * @fileOverview Invokes the verifier's get verification data API endpoint.
 */
import request from 'request-promise-native';
import * as constants from './constants';

/**
 * Returns verification data.
 * @param {string} customerID - Customer's access file identifier.
 * @return {Object} URL to verifier's iframe & tx reference.
 */
export default async (verificationTxReference) => {
    let response;
    try {
        response = await request({
            url: `${constants.URL_GET_STATUS}${verificationTxReference}/data`,
            headers: constants.HTTP_HEADERS_JSON,
            method: 'GET',
            auth: constants.HTTP_BASIC_AUTHENTICATION,
        });
    } catch(err) {
        if (err.statusCode === 403 || err.statusCode === 404) {
            return null;
        }
        throw err;
    }

    return JSON.parse(response);
}
