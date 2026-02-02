/**
 * @fileOverview Implements blake2b hashing algorithm.
 */

const hashFactory = require('blake2b');
import { getBuffer } from '../utils/conversion';

// Default length.
const LENGTH = 32;

/**
 * Returns hash of passed data using blake2b hashing algorithm.
 *
 * @param {Object} data - Data to be hashed.
 * @param {UInt8} length - Hash length (default = 32).
 * @return {Buffer} The hashed value.
 */
export default function(data, length=LENGTH) {
    const input = getBuffer(data);
    const h = hashFactory(length).update(input).digest();

    return Buffer.from(h);
}
