/**
 * @fileOverview Returns flag indicating whether verification process is complete or not.
 */
import request from 'request-promise-native';
import * as constants from './constants';

/**
 * Returns current processing status.
 * @param {String} verificationTxReference - Verifier's transaction reference.
 * @return {String} DONE | COMPLETE | UNKNOWN-TX.
 */
export default async (verificationTxReference) => {
    let response;
    try {
        response = await request({
            url: `${constants.URL_GET_STATUS}${verificationTxReference}`,
            headers: constants.HTTP_HEADERS_JSON,
            method: 'GET',
            auth: constants.HTTP_BASIC_AUTHENTICATION,
        });
    } catch(err) {
        if (err.statusCode === 403 || err.statusCode === 404) {
            return 'UNKNOWN-TX';
        }
        throw err;
    }

    const parsed = JSON.parse(response);

    return parsed.status;
}
