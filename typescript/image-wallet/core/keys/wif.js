/**
 * @fileOverview Wallet Import Format codec (https://en.bitcoin.it/wiki/Wallet_import_format).
 */

import * as bs58check from 'bs58check';
import { getBuffer } from '../utils/conversion';

// Byte appended if considered compressed.
const COMPRESSION_BYTE = 0x01;

/**
 * Encodes a private key in wallet import format.
 *
 * @param {hex} version - Addresses type, typically main = 0x80, test = 0xef.
 * @param {Buffer} privateKey - Coin symbol, e.g. AF.
 * @param {Boolean} compress - Flag inidcating whether private key will correspond to a compressed public key.
 * @return {String} wif - Private key encoded in wallet import format.
 */
export const encode = (version, privateKey, compress) => {
    const pvk = getBuffer(privateKey);
    if (pvk.length !== 32) {
        throw new TypeError('Invalid privateKey length')
    }

    // Initialise buffer (size depends on value of compress flag).
    const result = Buffer.allocUnsafe(compress ? 34 : 33);

    // 1 byte: version: 0x80 = mainnet address, 0xef = testnet address.
    result.writeUInt8(version, 0);

    // 32 bytes: private key.
    pvk.copy(result, 1);

    // 1 byte: 0x01 if private key will correspond to a compressed public key.
    if (compress) {
        result[33] = COMPRESSION_BYTE;
    }

    // Encode using bs58check: base58(result + (sha256 x 2).slice(0, 4)).
    return bs58check.encode(result);
}

/**
 * Decodes a private key from a wallet import formatted key.
 *
 * @param {String} encoded - The previously encoded key.
 * @param {hex} version - Addresses type, typically main = 0x80, test = 0xef.
 * @return {Buffer} pvk - Private key decoded from a wallet import format key.
 */
export const decode = (encoded, version) => {
    // Decode.
    encoded = bs58check.decode(encoded);

    // Validate decoded.
    if ([33, 34].includes(encoded.length) === false) {
        throw new Error('Invalid WIF length.');
    }
    if (version !== undefined && encoded[0] !== version) {
        throw new Error('Invalid network version');
    }
    if (encoded.length === 34 && encoded[33] !== COMPRESSION_BYTE) {
        throw new Error('Invalid compression flag');
    }

    return encoded.slice(1, 33);
}
