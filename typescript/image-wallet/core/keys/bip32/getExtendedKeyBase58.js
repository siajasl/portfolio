/**
 * @fileOverview Returns an extended key encoded in base58.
 */

import * as bs58check from 'bs58check';
import getExtendedKey from './getExtendedKey';

/**
 * Returns an extended key encoded in base58.
 * @param {HDKey} hdKey - An HD key pair.
 * @param {Buffer} version - Network's bip32 version.
 * @param {Buffer} key - Either the hd key's public or private key.
 * @return {Buffer} Extended key.
 */
export default (hdKey, version, key) => {
    const xkey = getExtendedKey(hdKey, version, key);

    return bs58check.encode(xkey)
};
