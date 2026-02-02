/**
 * @fileOverview Wraps cryptographic encryption functions.
 */

const argon2 = require('argon2-wasm');
import { blake2b } from '../hashing/index';
import { getBuffer } from '../utils/conversion';
import { IncorrectPasswordError } from '../utils/exceptions';

// Number of checksum bytes.
const CHECKSUM_BYTES = 2;

// Argon2 memory cost.
const ARGON2_MEM_COST = 512;

/**
 * Encrypts plain text.
 *
 * @param {Buffer|String} plainText - Text to be encrypted.
 * @param {Buffer|String} password - Password used to encrypt.
 * @param {Buffer|String} salt - Salt used for key derivation.
 * @param {Number} rounds - Number of rounds used for key derivation.
 * @return {Promise.<Buffer>} cipherText
 */
export const encrypt = async (plainText, password, salt, rounds) => {
    plainText = getBuffer(plainText);
    password = getBuffer(password);
    salt = getBuffer(salt);

    const checksum = getChecksum(plainText);
    const plainTextWithChecksum = Buffer.alloc(checksum.length + plainText.length);
    checksum.copy(plainTextWithChecksum);
    plainText.copy(plainTextWithChecksum, checksum.length);
    const key = await kdf(password, salt, rounds, plainTextWithChecksum.length);

    return xor(plainTextWithChecksum, key);
};

/**
 * Decrypts cipher text.
 *
 * @param {Buffer} cipherText - Text to be decrypted.
 * @param {string} password - Password used to encrypt.
 * @param {Buffer} salt - Salt used for key derivation.
 * @param {number} rounds - Number of rounds used for key derivation.
 * @throws IncorrectPasswordError - Raised when the supplied password is incorrect.
 * @return {Promise.<Buffer>} plainText
 */
export const decrypt = async (cipherText, password, salt, rounds) => {
    cipherText = getBuffer(cipherText);
    password = getBuffer(password);
    salt = getBuffer(salt);

    const key = await kdf(password, salt, rounds, cipherText.length);
    const decrypted = xor(cipherText, key);

    const plainText = decrypted.slice(CHECKSUM_BYTES);
    const checksum = getChecksum(plainText);
    if (checksum.compare(decrypted, 0, CHECKSUM_BYTES) !== 0) {
        throw new IncorrectPasswordError();
    }

    return plainText;
};

/**
 *
 * @param {Buffer} plainText
 * @return {Buffer} checksum
 */
const getChecksum = (plainText) => {
    return Buffer.from(blake2b(plainText).slice(0, CHECKSUM_BYTES));
};

/**
 * Derives an encryption key from a password.
 *
 * @param {string} password - Password to derive key from.
 * @param {Buffer} salt - Salt used for key derivation.
 * @param {number} rounds - Number of rounds used for key derivation.
 * @param {number} outputSize - Size of the derived key.
 * @return {Promise.<Uint8Array>} The derived key.
 */
const kdf = (password, salt, rounds, outputSize) => {
    return argon2.hash({
        pass: new Uint8Array(Buffer.from(password)),
        salt: new Uint8Array(salt),
        hashLen: outputSize,
        time: rounds,
        mem: ARGON2_MEM_COST
    }).then(res => res.hash);
};

/**
 *
 * @param {Buffer|Uint8Array} a
 * @param {Buffer|Uint8Array} b
 * @returns {Buffer}
 */
const xor = (a, b) => {
    const res = Buffer.alloc(a.length);
    for (let i = 0; i < a.length; ++i) {
        res[i] = a[i] ^ b[i];
    }

    return res;
};
