/**
 * @fileOverview Miscellaneous I/O utility functions.
 */

import { logError, logInfo } from './logging';

/**
 * Reads a File handle and returns contents.
 * @param {File} inputFile - a file handle.
 */
export const readFile = async (inputFile, actionType) => {
    const fr = new FileReader();
    return new Promise((resolve, reject) => {
        fr.onerror = () => {
            fr.abort();
            reject(new DOMException('Problem parsing input file.'));
        };
        fr.onload = () => {
            resolve(fr.result);
        };
        fr['readAs' + actionType](inputFile);
    });
};

/**
 * Reads a File handle and returns contents as an array buffer.
 * @param {File} inputFile - a file handle.
 */
export const readFileAsArrayBuffer = async (inputFile) => {
    return await readFile(inputFile, 'ArrayBuffer');
};

/**
 * Reads a File handle and returns contents as a binary string.
 * @param {File} inputFile - a file handle.
 */
export const readFileAsBinaryString = async (inputFile) => {
    return await readFile(inputFile, 'BinaryString');
};

/**
 * Reads a File handle and returns contents as a data URL.
 * @param {File} inputFile - a file handle.
 */
export const readFileAsDataURL = async (inputFile) => {
    return await readFile(inputFile, 'DataURL');
};

/**
 * Reads a File handle and returns contents as text.
 * @param {File} inputFile - a file handle.
 */
export const readFileAsText = async (inputFile) => {
    return await readFile(inputFile, 'Text');
};
