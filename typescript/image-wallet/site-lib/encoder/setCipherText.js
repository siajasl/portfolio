/**
 * @fileOverview Sets encrypted data to be transformed into a QR code.
 */

import Payload from '../utils/payload';

/**
 * Encrypts data in readiness for transformation to a QR code.
 * @param {EncodingContextInfo} ctx - Encoding processing context information.
 */
export default async function(ctx) {
    const payload = Payload.generate(ctx.purposeId);
    ctx.cipherText = await payload.encrypt(ctx.credentials.password);
}
