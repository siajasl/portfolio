/**
 * @fileOverview EdDSA ed25519 elliptic curve wrapper.
 */

import * as nacl from 'tweetnacl';
import { getBuffer, getUint8Array } from '../utils/conversion';

/**
 * Returns a key pair derived from supplied entropy.
 *
 * @param {String|Buffer|Array} seed - Randomness with sufficient entropy.
 * @return {Object} A key pair for signing / verification purposes.
 */
export const getKeyPair = (seed) => {
    seed = getUint8Array(seed);
	const {
        publicKey,
        secretKey: privateKey
    } = nacl.sign.keyPair.fromSeed(seed);

    return {
        publicKey: getBuffer(publicKey),
        privateKey: getBuffer(privateKey),
    }
};

/**
 * Returns a private key derived from supplied entropy.
 *
 * @param {String|Buffer|Array} seed - Randomness with sufficient entropy.
 * @return {Buffer} A private key (64 byte buffer).
 */
export const getPrivateKey = (seed) => {
    const { privateKey } = getKeyPair(seed);

    return privateKey;
};

/**
* Returns a public key derived from supplied entropy.
 *
 * @param {String|Buffer|Array} entropy - Randomness with sufficient entropy.
 * @return {Buffer} A public key (32 byte buffer).
 */
export const getPublicKey = (seed) => {
    const { publicKey } = getKeyPair(seed);

    return publicKey;
};

/**
 * Returns a digital signature of a hashed message.
 *
 * @param {Object|String|Buffer|Array|Uint8Array} key - Either a key pair or a private key.
 * @param {String|Buffer|Array|Uint8Array} msgHash - Hash of message being signed.
 * @return {Buffer} A digital signature as a byte array.
 */
export const sign = (key, msgHash) => {
    key = getUint8Array(key.privateKey || key);
    msgHash = getUint8Array(msgHash);
    const sig = nacl.sign.detached(msgHash, key);

    return getBuffer(sig);
};

/**
 * Verifies a hashed message signature .
 *
 * @param {String|Buffer|Array|Uint8Array} key - A verification key used to verify a signature.
 * @param {String|Buffer|Array|Uint8Array} msgHash - Hexadecimal string.
 * @param {String|Buffer|Array|Uint8Array} sig - A digital signature of the message hash in DER array format.
 * @return {Boolean} True if verified, false otherwise.
 */
export const verify = (key, msgHash, sig) => {
    return nacl.sign.detached.verify(
        getUint8Array(msgHash),
        getUint8Array(sig),
        getUint8Array(key)
    )
};
