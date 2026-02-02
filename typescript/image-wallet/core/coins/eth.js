/**
 * @fileOverview Coin metadata plus related functions.
 */

import { keccak256 } from '../hashing/index';
import { secp256k1 } from '../ecc/index';
import { getBuffer } from '../utils/conversion';

// Elliptic curve name.
export const curve = 'secp256k1';

// SLIP 0044 coin index.
export const index = 60;

// SLIP 0044 coin hex code.
export const hexCode = 0x8000003c;

// Coin name.
export const name = 'Ether';

// Coin ticker symbol.
export const symbol = 'ETH';

/**
 * Returns an Ethereum address mapped from a private key.
 *
 * @param {Array} pvk - A private key.
 * @return {hex} The address.
 */
export const getAddressFromPrivateKey = (pvk) => {
    // Strip leading 0x.
    pvk = pvk.length === 66 ? pvk.slice(2) :
          pvk.length === 33 ? pvk.slice(1) : pvk;

    // Map private key --> (uncompressed) public key.
    pvk = getBuffer(pvk);
    const pbk = secp256k1.getPublicKeyFromPrivate(pvk, false);

    return getAddressFromPublicKey(pbk.slice(1));
};

/**
 * Returns an Ethereum address mapped from a public key.
 *
 * @param {Array} pbk - A public key.
 * @return {hex} The address.
 */
export const getAddressFromPublicKey = (pbk) => {
    // Map public key --> keccak256 hash.
    const pbkh = keccak256(pbk);

    // Map keccak256 hash --> address.
    const addr = pbkh.slice(12).toString('hex');

    return `0x${addr}`;
};
