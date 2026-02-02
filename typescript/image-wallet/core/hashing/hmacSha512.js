/**
 * @fileOverview Implements sha512 hashing algorithm.
 */

import { createHmac as hashFactory } from 'crypto';
import { getBuffer } from '../utils/conversion';

// Hashing algorithm.
const ALGO = 'sha512';

/**
 * Authenticates the given message with the secret key, i.e. returns HMAC-SHA-512 of the message under the key.
 *
 * @param {Object} data - Data to be hashed.
 * @return {Buffer} The hashed value.
 */
export default function(data, key) {
    const input = getBuffer(data);

    return hashFactory(ALGO, input).update(key).digest();
}
