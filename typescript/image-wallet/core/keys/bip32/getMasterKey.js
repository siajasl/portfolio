/**
 * @fileOverview Maps a seed & network metadata to a master key.
 */

import { BigNumber as BN } from 'bignumber.js';
import { hmacSha512 } from '../../hashing/index';
import { EllipticalCurveType } from '../../ecc/index';

/**
 * Maps a master seed to a master private key + chain code.
 * @param {Buffer} seed - Typically a 64 byte seed derived from 32 bytes of entropy via bip39.
 * @param {NetworkInfo} network - Network specific metadata.
 * @return {object} A master private key/chain-code.
 */
export default (seed, network) => {
    const I = derive(seed, network);

    return {
        key: I.slice(0, 32),
        chainCode: I.slice(32)
    }
};

/**
 * Returns a master key derived from a seed & curve type.
 */
const derive = (seed, network) => {
    switch (network.curve.type) {
        case EllipticalCurveType.ed25519:
            return derive_ed25519(seed, network);
        case EllipticalCurveType.secp256k1:
            return derive_secp256k1(seed, network);
        default:
            throw new Error('Network curve type unsupported.');
    }
};

/**
 * Returns an ed25519 compliant master key.
 */
const derive_ed25519 = (seed, network) => {
    // Destructure elliptic curve params.
    const { curve: { seedModifierBuffer } } = network;

    // Calculate hmac.
    const I = hmacSha512(seedModifierBuffer, seed);

    return I;
};

/**
 * Returns a secp256k1 compliant master key.
 */
const derive_secp256k1 = (seed, network) => {
    // Destructure elliptic curve params.
    const { curve: { order, seedModifierBuffer } } = network;

    // Iterate until private key is valid.
    let I;
    let data = seed;
    while (true) {
        // Calculate hmac.
        I = hmacSha512(seedModifierBuffer, data);

        // Apply curve order check.
        let a = BN(`0x${I.slice(0, 32).toString('hex')}`);
        if (!a.eq(0) && a.lt(order)) {
            break;
        }

        // Prepare for next loop.
        data = I;
    }

    return I
};
