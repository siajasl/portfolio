/**
 * @fileOverview Implements keccak256 hashing algorithm.
 */

import { default as hashFactory } from 'sha.js';
import { getBuffer } from '../utils/conversion';

// Hashing algorithm - sha-2:sha256.
const ALGO = 'sha256';

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
