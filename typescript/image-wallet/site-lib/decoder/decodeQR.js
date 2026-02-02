/**
 * @fileOverview Decodes the QR code from an access file.
 */

import { isString } from 'lodash';
import * as fs from 'fs';
import jsQR from 'jsqr';
import PNGReader from 'png.js';
import * as exceptions from '../utils/exceptions';
import { readFileAsArrayBuffer, readFileAsDataURL } from '../utils/io';

/**
 * Decodes QR code embedded in an access file.
 * @param {File|String} source - An access file file.
 * @return {Buffer} - QR code payload.
 */
export default async (source) => {
    const decoder = isString(source) ? decodeFilePath : decodeFileHandle;
    const decoded = await decoder(source);

    return Buffer.from(decoded.binaryData);
}

/**
 * Decodes QR code embedded in access file.
 * @param {File} fhandle - An access file file handle.
 * @return {Buffer} - QR code payload.
 */
const decodeFileHandle  = async (fhandle) => {
    const buffer = await readFileAsArrayBuffer(fhandle);

    return await decodeFromFileBuffer(buffer);
}

/**
 * Decodes QR code embedded in access file.
 * @param {File} fhandle - An access file file handle.
 * @return {Buffer} - QR code payload.
 */
const decodeFilePath  = async (fpath) => {
    if (fs.existsSync(fpath) === false) {
        throw Error(`Invalid access file file path: ${fpath}`)
    }
    const fbuffer = fs.readFileSync(fpath);

    return await decodeFromFileBuffer(fbuffer);
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
