/**
 * @fileOverview Initialises access file settings.
 */

import * as fs from 'fs';
import { AF } from '../imports'

/**
 * Initialises access file settings.
 * @param {String} fpath - Path to a dlt access file.
 * @returns {String} - Access file seed in hexadecimal.
 */
export default async (fpath, pwd=process.env.TRADE_CLI_AF_PWD) => {
    // Set password.
    if (!pwd) {
        throw Error('Invalid access file password')
    }

    const { seed } = await AF.decrypt(fpath, pwd);

    return seed;
}
