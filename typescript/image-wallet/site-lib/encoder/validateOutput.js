/**
 * @fileOverview Decodes the QR code from an access file.
 */

import jsQR from 'jsqr';
import PNGReader from 'png.js';
import * as exceptions from '../utils/exceptions';
const dataUriToBuffer = require('data-uri-to-buffer');

/**
 * Returns flag inidcating whether encoding output is valid or not.
 * @param {DecodingContextInfo} ctx - Encoding processing context information.
 */
export default async function(ctx) {
    const asDataURL = ctx.$canvas.toDataURL();
    const asBuffer = dataUriToBuffer(asDataURL);
    const asDecoded = await decodeFromFileBuffer(asBuffer);

    return asDecoded !== null;
}

/**
 * Extract qr data from PNG file buffer.
 * @param {ArrayBuffer} buffer - PNG file array buffer.
 */
const decodeFromFileBuffer = async (buffer) => {
    return new Promise((resolve, reject) => {
        const pr = new PNGReader(buffer);
        pr.parse((err, pngData) => {
            if (err) {
                reject(new exceptions.InvalidPngFileError(err));
            }
            const pixelArray = convertPngToByteArray(pngData);
            resolve(jsQR(pixelArray, pngData.width, pngData.height));
        });
    });
};

/**
 * Convert PNG data to a byte array.
 * @param {PNGReader.Output} pngData - Data emitted from PNG parser.
 */
const convertPngToByteArray = (pngData) => {
    const arr = new Uint8ClampedArray(pngData.width * pngData.height * 4);
    for (let y = 0; y < pngData.height; y++) {
        for (let x = 0; x < pngData.width; x++) {
            const pixelData = pngData.getPixel(x, y);
            arr[(y * pngData.width + x) * 4 + 0] = pixelData[0];
            arr[(y * pngData.width + x) * 4 + 1] = pixelData[1];
            arr[(y * pngData.width + x) * 4 + 2] = pixelData[2];
            arr[(y * pngData.width + x) * 4 + 3] = pixelData[3];
        }
    }
    return arr;
};
