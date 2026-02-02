/**
 * @fileOverview QR code payload encryption/decryption.
 */

import * as AFC from '@trinkler/accessfile-core';
import * as exceptions from '../utils/exceptions';

// Number of entropic bytes.
const ENTROPY_BYTES = 32;

// Number of salt bytes.
const SALT_BYTES = 16;

// Number of rounds used for key derivation.
const KDF_ROUNDS = 256;

// Payload format version.
const FORMAT_VERSION = 3;

export default class Payload {
    /**
     * @param {number} purposeId
     * @param {Buffer} entropy
     */
    constructor(purposeId, entropy) {
        this._purposeId = purposeId;
        this._entropy = entropy;
    }

    /**
     *
     * @param {number} purposeId
     * @return {Payload}
     */
    static generate(purposeId) {
        // TODO validate that purposeId is uint32
        const entropy = AFC.getEntropy(ENTROPY_BYTES);

        return new Payload(purposeId, entropy);
    }

    /**
     * @param {Buffer} encrypted
     * @param {string} password
     * @return {Payload}
     * @throws {UnsupportedVersionError}
     */
    static async decrypt(encrypted, password) {
        // Destructure payload format version.
        const version = encrypted[0];
        if (version !== FORMAT_VERSION) {
            throw new exceptions.UnsupportedVersionError();
        }

        // Destructure kdf rounds.
        const kdfRoundsLog2 = encrypted[1];
        if (kdfRoundsLog2 > 32) {
            throw new exceptions.ExcessiveRoundsError();
        }
        const kdfRounds = Math.pow(2, kdfRoundsLog2);

        // Destructure salt.
        const salt = encrypted.slice(2, 2 + SALT_BYTES);

        // Destructure cipher text.
        const cipherText = encrypted.slice(2 + SALT_BYTES);

        // Decrypt plain text.
        const plainText = await AFC.decrypt(cipherText, password, salt, kdfRounds);

        // Destructure purpose id + entropy.
        const purposeId = plainText.readUInt32BE(0);
        const entropy = plainText.slice(4);

        // Return payload wrapper.
        return new Payload(purposeId, entropy);
    }

    /**
     *
     * @param {string} password
     * @returns {Promise.<Buffer>}
     */
    async encrypt(password) {
        // Set plain text: purpose id + entropy.
        const plainText = Buffer.alloc(/*purposeId*/ 4 + ENTROPY_BYTES);
        plainText.writeUInt32BE(this._purposeId, 0);
        this._entropy.copy(plainText, 4);

        // Set cipher text.
        const salt = AFC.getEntropy(SALT_BYTES);
        const kdfRounds = KDF_ROUNDS;
        const cipherText = await AFC.encrypt(plainText, password, salt, kdfRounds);

        // Set cipher text with header.
        const cipherTextWithHeader = Buffer.alloc(
            /*version*/ 1
            + /*kdf rounds*/ 1
            + /*salt*/ SALT_BYTES
            + cipherText.length
        );
        cipherTextWithHeader[0] = FORMAT_VERSION;
        cipherTextWithHeader[1] = Math.log2(KDF_ROUNDS);
        salt.copy(cipherTextWithHeader, 2);
        cipherText.copy(cipherTextWithHeader, 2 + SALT_BYTES);

        return cipherTextWithHeader;
    }

    /**
     * @type {Buffer}
     */
    get entropy() {
        return this._entropy;
    }

    /**
     * @type {number}
     */
    get purposeId() {
        return this._purposeId;
    }
}
