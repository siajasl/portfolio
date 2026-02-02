/**
 * @fileOverview Returns a public key hash - see https://learnmeabitcoin.com/glossary/public-key-hash160.
 */

import ripemd160 from './ripemd160';
import sha256 from './sha256';

import { getBuffer } from '../utils/conversion';

/**
 * Returns hash of an secp256k1 public key.
 *
 * @param {Object} data - Data to be hashed.
 * @return {Buffer} The hashed value.
 */
export default function(data) {
    return ripemd160(sha256(getBuffer(data)));
}
