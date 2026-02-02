/**
 * @fileOverview Library type instance converstion utility functions.
 */

import { isObject, isString, isTypedArray } from 'lodash';
import isHex from 'is-hex';

/**
 * Converts & returns a Buffer decoded from some data.
 *
 * @param {Object} data - Data to be converted to a Buffer.
 * @return Decoded Buffer.
 */
export const getBuffer = (data) => {
    if (Buffer.isBuffer(data)) {
        return data;
    }

    if (Array.isArray(data)) {
        return Buffer.from(data);
    }

    if (isTypedArray(data)) {
        return Buffer.from(data);
    }

    if (isString(data)) {
        if (isHex(data)) {
            return Buffer.from(data, 'hex');
        } else {
            return Buffer.from(data, 'utf8');
        }
    }

    if (isObject(data)) {
        return Buffer.from(JSON.stringify(data, null, 0), 'utf8');
    }

    throw new TypeError('Cannot convert data to a buffer');
}

/**
 * Converts & returns a Uint8Array decoded from some data.
 *
 * @param {Object} data - Data to be converted to a Uint8Array.
 * @return Decoded Uint8Array.
 */
export const getUint8Array = (data) => {
    return Uint8Array.from(getBuffer(data));
}
