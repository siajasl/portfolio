/**
 * @fileOverview ECDSA secp256k1 elliptic curve wrapper.
 * see - https://eng.paxos.com/blockchain-101-elliptic-curve-cryptography
 */

const elliptic = require('elliptic');
import { isArray, isBuffer } from 'lodash';
import { getBuffer } from '../utils/conversion';

// Set ECDSA context.
const CURVE = new elliptic.ec('secp256k1');

// Order (n).
export const order = '0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141';

/**
 * Returns a key pair derived from supplied entropy.
 *
 * @param {String|Buffer|Array} seed - Randomness with sufficient entropy.
 * @return {Object} A key pair for signing / verification purposes.
 */
export const getKeyPair = (seed) => {
    seed = getBuffer(seed).toString('hex');
    const kp = CURVE.genKeyPair({entropy:seed});

    return {
        privateKey: kp.priv.toBuffer(),
        publicKey: getBuffer(kp.getPublic(true, 'array'))
    }
};

/**
 * Returns a private key derived from supplied seed.
 *
 * @param {String|Buffer|Array} seed - Randomness with sufficient entropy.
 * @return {Buffer} A private key (64 bytes).
 */
export const getPrivateKey = (seed) => {
    const { privateKey } = getKeyPair(seed);

    return privateKey;
};

/**
* Returns a public key derived from supplied seed.
 *
 * @param {String|Buffer|Array} seed - Randomness with sufficient entropy.
 * @return {Buffer} A private key (32 bytes).
 */
export const getPublicKey = (seed) => {
    const { publicKey } = getKeyPair(seed);

    return publicKey;
};

/**
* Returns a public key derived from a private key.
 *
 * @param {String|Buffer|Array} pvk - A private key.
 * @param {Boolean} compress - Flag indicating whether to return a compressed (33 byte) or uncompressed (65 byte) key.
 * @return {Buffer} A public key buffer.
 */
export const getPublicKeyFromPrivate = (pvk, compress) => {
    compress = compress === undefined ? true : compress;
    const kp = CURVE.keyFromPrivate(getBuffer(pvk));
    const pbk = kp.getPublic().encode('array', compress);

    return getBuffer(pbk);
};

/**
 * Returns a digital signature of a hashed message.
 *
 * @param {Object|String|Buffer|Array} key - Either a key pair or a private key.
 * @param {String|Buffer|Array} msgHash - Hexadecimal string.
 * @return {Buffer} A digital signature as a byte array.
 */
export const sign = (pvk, msgHash) => {
    const kp = CURVE.keyFromPrivate(pvk);
    const sig = kp.sign(msgHash);

    return getBuffer(sig.toDER());
};

/**
 * Verifies a hashed message signature .
 *
 * @param {String|Buffer|Array} publicKey - A public key used to verify a signature.
 * @param {String|Buffer|Array} msgHash - Hexadecimal string.
 * @param {String|Buffer|Array} sig - A digital signature of the message hash in DER array format.
 * @return {Boolean} True if verified, false otherwise.
 */
export const verify = (key, msgHash, sig) => {
    key = getBuffer(key);
    msgHash = getBuffer(msgHash);
    sig = getBuffer(sig);
    const verifier = CURVE.keyFromPublic(key);

    return verifier.verify(msgHash, sig);
};
