import * as fs from 'fs';
import * as path from 'path';
import * as cache from '../cache';
import { AFC } from '../imports';
import { logError, logInfo } from '../logging';
import {
    DIR_CLEARING_ERROR,
    DIR_CLEARING_OPEN
} from './constants';

// Number of entropic bytes.
const ENTROPY_BYTES = 32;

// Number of salt bytes.
const SALT_BYTES = 16;

// Number of rounds used when encrypting.
const KDF_ROUNDS = 256;

// Plain text encoding format.
const UTF8 = 'utf8';

/**
 * Reads a redemption secret from a settlement information file.
 * @param {String} settlementID - System settlement identifier.
 * @returns {String} Settlement redemption secret.
 */
export const readRedemptionSecret = async (settlementID) => {
    const { redeemSecret } = await readFileEncrypted(DIR_CLEARING_OPEN, settlementID);

    return redeemSecret;
}

/**
 * Writes information to file system information upon a settlement initiation event.
 * @param {String} settlementID - System settlement identifier.
 * @param {String} secret - Secret used to trigger subsequent redemption.
 */
export const writeOnInitiation = async (settlementID, secret) => {
    await writeFileEncrypted(DIR_CLEARING_OPEN, settlementID, {
        settlementID,
        redeemSecret: secret
    });
}

/**
 * Moves settlement information upon a settlement error event.
 * @param {String} settlementID - System settlement identifier.
 */
export const writeOnError = (settlementID, error) => {
    removeFile(DIR_CLEARING_OPEN, settlementID);
    writeFile(DIR_CLEARING_ERROR, settlementID, {
        settlementID,
        error
    });
}

/**
 * Moves settlement information upon a settlement finalisation event.
 * @param {String} settlementID - System settlement identifier.
 */
export const writeOnFinalisation = (settlementID) => {
    moveFile(
        getFilePath(DIR_CLEARING_OPEN, settlementID),
        getFilePath(DIR_CLEARING_PROCESSED, settlementID)
    )
}

/**
 * Moves settlement information upon a settlement timeout event.
 * @param {String} settlementID - System settlement identifier.
 */
export const writeOnTimeout = (settlementID) => {
    moveFile(
        getFilePath(DIR_CLEARING_OPEN, settlementID),
        getFilePath(DIR_CLEARING_ERROR_TIMEOUT, settlementID)
    )
}

/**
 * Returns file system path to which a settlement information file will be written.
 * @param {String} directory - Directory to which information will be written.
 * @param {String} settlementID - System settlement identifier.
 */
const getFilePath = (directory, settlementID) => {
    return path.join(directory, `${settlementID}.json`);
}

/**
 * Moves settlement information file from one managed directory to another.
 * @param {String} oldPath - Path of existing settlement information file.
 * @param {String} newPath - Path of new settlement information file.
 */
const moveFile = (oldPath, newPath) => {
    fs.renameSync(oldPath, newPath);
    logInfo(`settlement information moved to: ${newPath}`);
}

/**
 * Read a settlement information file into memory.
 * @param {String} directory - Directory to which settlement information file was written.
 * @param {String} settlementID - System settlement identifier.
 */
const readFile = (directory, settlementID) => {
    const fpath = getFilePath(directory, settlementID);
    const fdata = fs.readFileSync(fpath);

    return JSON.parse(fdata);
}

/**
 * Reads an encrypted settlement information file into memory.
 * @param {String} directory - Directory to which settlement information file was written.
 * @param {String} settlementID - System settlement identifier.
 */
const readFileEncrypted = async (directory, settlementID) => {
    const { data } = readFile(directory, settlementID);
    const envelope = Buffer.from(data);
    const kdfRoundsLog2 = envelope[0];
    const kdfRounds = Math.pow(2, kdfRoundsLog2);
    const salt = envelope.slice(1, 1 + SALT_BYTES);
    const cipherText = envelope.slice(1 + SALT_BYTES);
    const password = cache.getAccessFilePrivateKey().toString('hex');
    const plainText = await AFC.encryption.decrypt(cipherText, password, salt, kdfRounds);

    return JSON.parse(plainText);
}

/**
 * Read a settlement information file into memory.
 * @param {String} directory - Directory to which settlement information file was written.
 * @param {String} settlementID - System settlement identifier.
 */
const removeFile = (directory, settlementID) => {
    const fpath = getFilePath(directory, settlementID);
    try {
        fs.unlinkSync(fpath)
    } catch(err) {
        logError(err);
    }
}

/**
 * Writes a settlement information file to file system.
 * @param {String} directory - Directory to which settlement information file will be written.
 * @param {String} settlementID - System settlement identifier.
 */
const writeFile = (directory, settlementID, data) => {
    const fpath = getFilePath(directory, settlementID);
    fs.writeFileSync(fpath, JSON.stringify(data));
    logInfo(`settlement information written to: ${fpath}`);
}

/**
 * Writes an encrypted settlement information file to file system.
 * @param {String} directory - Directory to which settlement information file will be written.
 * @param {String} settlementID - System settlement identifier.
 * @param {Object} data - Data to be encrypted.
 */
const writeFileEncrypted = async (directory, settlementID, data) => {
    // Set cipher text.
    const plainText = Buffer.from(JSON.stringify(data), UTF8);
    const password = cache.getAccessFilePrivateKey().toString('hex');
    const salt = await AFC.getEntropy(SALT_BYTES);
    const cipherText = await AFC.encryption.encrypt(plainText, password, salt, KDF_ROUNDS);

    // Set cipher envelope.
    const envelope = Buffer.alloc(1 + SALT_BYTES + cipherText.length);
    envelope[0] = Math.log2(KDF_ROUNDS);
    salt.copy(envelope, 1);
    cipherText.copy(envelope, 1 + SALT_BYTES);

    // Write cipher envelope.
    writeFile(directory, settlementID, envelope);
}
