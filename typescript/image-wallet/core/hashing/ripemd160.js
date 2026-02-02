/**
 * @fileOverview Implements keccak256 hashing algorithm.
 */

import { default as hashFactory } from 'ripemd160';
import { getBuffer } from '../utils/conversion';

/**
 * Returns 32 byte hash of passed data using keccak256 hashing algorithm.
 *
 * @param {Object} data - Data to be hashed.
 * @return {Buffer} The hashed value.
 */
export default function(data) {
    const input = getBuffer(data);

    return new hashFactory().update(input).digest();
}
