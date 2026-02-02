/**
 * @fileOverview Derives a so-called extended key.
 */

import { EllipticalCurveType } from '../../ecc/index';

/**
 * Returns an extended key.
 * @param {HDKey} hdKey - An HD key pair.
 * @param {Buffer} version - Network's bip32 version.
 * @param {Buffer} key - Either the hd key's public or it's private key.
 * @return {Buffer} Extended key.
 */
export default (hdKey, version, key) => {
    // Destructure hd key.
    const { chainCode, depth, network, node, parentFingerprint } = hdKey;

    // Initialise 78 bytes buffer.
    const result = Buffer.allocUnsafe(78);

    // 4 bytes: version bytes.
    result.writeUInt32BE(version, 0);

    // 1 byte: depth: 0x00 for master nodes, 0x01 for level-1 descendants, ....
    result.writeUInt8(depth, 4);

    // 4 bytes: the fingerprint of the parent's key (0x00000000 if master key).
    parentFingerprint.copy(result, 5);

    // 4 bytes: child number. This is the number i in xi = xpar/i, with xi the key being serialized.
    result.writeUInt32BE(node.index, 9);

    // 32 bytes: the chain code.
    chainCode.copy(result, 13);

    // 33 bytes: the key.
    switch (network.curve.type) {
        // ed25519 pvk/pbk length = 32, therefore left pad with 0x00.
        case EllipticalCurveType.ed25519:
            result.writeUInt8(0, 45);
            key.copy(result, 46);
            break;

        // secp256k1 pvk length = 32, therefore left pad with 0x00.
        case EllipticalCurveType.secp256k1:
            if (key.length === 32) {
                result.writeUInt8(0, 45);
                key.copy(result, 46);
            } else {
                key.copy(result, 45);
            }
            break;

        default:
            throw new Error('Network curve type unsupported.');
    }

    return result;
};
