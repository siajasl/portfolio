/**
 * @fileOverview Implements keccak256 hashing algorithm.
 */

import { default as hashFactory } from 'keccak';
import { getBuffer } from '../utils/conversion';

// Hashing algorithm - sha-3:keccak256.
const ALGO = 'keccak256';

/**
 * Returns 32 byte hash of passed data using keccak256 hashing algorithm.
 *
 * @param {Object} data - Data to be hashed.
 * @return {Buffer} The hashed value.
 */
export default function(data) {
    const input = getBuffer(data);

    return hashFactory(ALGO).update(input).digest();
}
