/**
 * @fileOverview Maps a parent key plus a derivation node to a derived child key.
 */

import { BigNumber as BN } from 'bignumber.js';
import { hmacSha512 } from '../../hashing/index';
import { EllipticalCurveType } from '../../ecc/index';

// Limit beyond which an index is considered to be 'hardened'.
const HIGHEST_BIT = 0x80000000;

// Buffer(s) prepended to derived keys.
const ZERO_BYTE = Buffer.alloc(1, 0);
const ONE_BYTE = Buffer.alloc(1, 1);


/**
 * Maps a master seed to a master private key + chain code.
 * @param {HDKey} parent - Parent HD key.
 * @param {HDNode} node - A node within an HD derivation path.
 * @return {object} A master private key/chain-code.
 */
export default (parent, node) => {
    const data = getDerivationData(parent, node);
    const {IL, IR} = derive(parent, node, data);

    return {key: IL, chainCode: IR};
};

/**
 * Returns initial derivation data.
 */
const getDerivationData = (parent, node) => {
    // Hardened: 0x00 || ser256(kpar) || ser32(index)
    if (node.index >= HIGHEST_BIT) {
        return Buffer.concat([ZERO_BYTE, parent.privateKey, node.indexBuffer])

    // Unhardened: serP(Kpar) || ser32(index)
    } else {
        return Buffer.concat([parent.publicKey, node.indexBuffer]);
    }
}

/**
 * Returns a child key derived from it's parent & derivation node.
 */
const derive = (parent, node, data) => {
    const { network } = parent;
    switch (network.curve.type) {
        case EllipticalCurveType.ed25519:
            return derive_ed25519(parent, node, data);
        case EllipticalCurveType.secp256k1:
            return derive_secp256k1(parent, node, data);
        default:
            throw new Error('Network curve type unsupported.');
    }
};

/**
 * Returns an secp256k1 compliant key derived from it's parent & an index.
 */
const derive_secp256k1 = (parent, node, data) => {
    let a, k, pvk, I, IL, IR;
    const { curve: { order } } = parent.network;

    // Set BN represenation of parent private key.
    pvk = BN(`0x${parent.privateKey.toString('hex')}`);

    while (true) {
        // Calculate hmac.
        I = hmacSha512(parent.chainCode, data);
        IL = I.slice(0, 32);
        IR = I.slice(32);

        // Apply curve order check.
        a = BN(`0x${IL.toString('hex')}`);
        k = a.plus(pvk).mod(order);
        if (!k.eq(0) && a.lt(order)) {
            k = k.toString(16);
            k = k.length === 63 ? `0${k}` : k;
            IL = Buffer.from(k, 'hex');
            break;
        }

        // Set data for next iteration.
        data = Buffer.concat([ONE_BYTE, IR, node.indexBuffer]);
    }

    return {IL, IR};
};

/**
 * Returns an ed25519 compliant key derived from it's parent & an index.
 */
const derive_ed25519 = (parent, node, data) => {
    const I = hmacSha512(parent.chainCode, data);
    const IL = I.slice(0, 32);
    const IR = I.slice(32);

    return {IL, IR};
};
