/**
 * @fileOverview Coin metadata plus related functions.
 */

import * as bs58 from 'bs58';
import { hash160, sha256 } from '../hashing/index';
import { secp256k1 } from '../ecc/index';
import { getBuffer } from '../utils/conversion';

// BIP32 related metadata.
export const bip32 = {
    versions: {
        private: 0x0488ADE4,
        public: 0x0488B21E
    }
};

// Elliptic curve name.
export const curve = 'secp256k1';

// SLIP 0044 coin index.
export const index = 0;

// SLIP 0044 coin hex code.
export const hexCode = 0x80000000;

// Coin name.
export const name = 'Bitcoin';

// Coin ticker symbol.
export const symbol = 'BTC';

// Wallet import format related metadata.
export const wif = {
    version: 0x80
};

/**
 * Returns an Ethereum address mapped from a private key.
 *
 * @param {Array} pvk - A private key.
 * @return {hex} The address.
 */
export const getAddressFromPrivateKey = (pvk) => {
    // Map private key --> (compressed) public key.
    pvk = getBuffer(pvk);
    const pbk = secp256k1.getPublicKeyFromPrivate(pvk, true);

    return getAddressFromPublicKey(pbk);
};

/**
 * Maps public key to a network address.
 *
 * @param {Array} pbk - A public key (compressed).
 * @param {Number} version - Network version identifier.
 * @return {String} A network address.
 */
export const getAddressFromPublicKey = (pbk, version) => {
    // Mutate public key.
    pbk = hash160(pbk);

    // Set version.
    version = version || Buffer.alloc(1, 0);

    // Set a: public key + version.
    const a = Buffer.concat([version, pbk]);

    // Set b: fingerprint.
    const b = sha256(sha256(a)).slice(0, 4);

    // Set c: a + b.
    const c = Buffer.concat([a, b]);

    // Return base 58 encoding of c.
    return bs58.encode(c);
};
