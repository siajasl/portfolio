/**
 * @fileOverview Invokes the verifier's get verification data API endpoint.
 */

import request from 'request-promise-native';
import * as constants from './constants';

// Map of scan type to url suffixes.
const SCANTYPE_URL_SUFFIXES = {
    'id-frontis': '/images/front',
    'selfie': '/images/face'
}

/**
 * Returns verification data.
 * @param {String} verificationTxReference - Verifier's transaction reference.
 * @param {String} scanType - Type of scan being pulled.
 * @return {String} Image as data URL.
 */
export default async (verificationTxReference, scanType) => {
    const url_prefix = SCANTYPE_URL_SUFFIXES[scanType];
    const url = `${constants.URL_GET_SCANS}${verificationTxReference}${url_prefix}`;

    try {
        return await request({
            url: url,
            headers: constants.HTTP_HEADERS_IMG,
            method: 'GET',
            auth: constants.HTTP_BASIC_AUTHENTICATION,
        });
    } catch(err) {
        if (err.statusCode === 403 || err.statusCode === 404) {
            return null;
        }
        throw err;
    }
}
