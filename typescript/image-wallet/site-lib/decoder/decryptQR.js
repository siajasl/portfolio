/**
 * @fileOverview Sets decrypted data, i.e. user private key.
 */

import Payload from '../utils/payload';

/**
 * Decrypts QR code.
 * @param {Buffer} qrData - Encrypted QR code.
 * @param {String} password - Password used for decryption.
 * @return {{seed: Buffer, purposeId: number}} - Decrypted payload.
 */
export default async (qrData, password) => {
    const { entropy: seed, purposeId } = await Payload.decrypt(qrData, password);

    return { seed, purposeId };
}
