/**
 * @fileOverview An access file - easier than brain wallets.
 *
 * @exports decode/deriveKey/encode/name/provider/version
 * @version 0.7.0
 */

import decoder from './decoder/decodeQR';
import decryptor from './decoder/decryptQR';
import encoder from './encoder/index';

// Library metadata.
export const NAME = 'Accessfile';
export const PROVIDER = 'Trinkler Software AG';
export const VERSION = '0.7.0';

/**
 * Creates a lightly branded access file
 * @param {object} password - A user's (hopefully strong) password.
 * @param {number} purposeId - A number identifying the purpose of this wallet.
 * @param {object} options - Encoding options.
 * @return A promise resolving to an HTML canvas object.
 */
export const create = async (password, purposeId, options) => {
    return await encoder({ password }, purposeId, options);
};

/**
 * Decrypts an access file.
 * @param {File|String} source - Access file handle | path.
 * @param {String} password - Password used when generating wallet.
 * @return {{seed: String, purposeId: Number}} - Decrypted access file payload.
 */
export const decrypt = async (source, password) => {
    const decoded = await decoder(source);
    const { seed, purposeId } = await decryptor(decoded, password);

    return {
        seed: seed.toString('hex'),
        purposeId
    }
}
