/**
 * @fileOverview Wraps CLI settings I/O operations.
 */

import * as fs from 'fs';
import { FILE_SETTINGS as FILE } from './constants';

/**
 * Returns flag indicating whether settings.json file exists.
 */
export const exists = () => {
    return fs.existsSync(FILE);
}

/**
 * Reads settings from file system.
 */
export const read = async () => {
    const fstream = fs.readFileSync(FILE, 'utf8');
    return await JSON.parse(fstream);
}

/**
 * Writes settings to file system.
 * @param {Object} settings - Settings to written to file system.
 */
export const write = async (settings) => {
    const asJSON = await JSON.stringify(settings);
    fs.writeFileSync(FILE, asJSON);

    return settings;
}
