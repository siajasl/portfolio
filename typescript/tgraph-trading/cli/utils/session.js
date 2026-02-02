/**
 * @fileOverview Application session manager.
 */

import { API, AFC } from './imports';
import * as cache from './cache';
import * as io from './io/index';

// Access file ticker symbol.
const AF_SYMBOL = 'AF';

// Access file account index.
const AF_ACCOUNT_INDEX = 0;

/**
 * Session initialiser.
 */
export const init = async () => {
    // Setup file system directories.
    await io.initFileSystem();

    // Load settings.
    const settings = await io.getSettings();

    // Load access file.
    await setAccessFileSettings(settings);

    // Initialise cache.
    cache.init(settings);

    // Initialise API client.
    await API.initialise(settings.accessfile.keyPair);
};

/**
 * Extends loaded settings with access file related data.
 */
const setAccessFileSettings = async (settings) => {
    if (settings.accessfile.path === 'PATH-TO-ACCESS-FILE') {
        return;
    }

    // Load entropy encoded within access file.
    let seed = await io.getAccessFile(settings.accessfile.path);
    seed = Buffer.from(seed, 'hex');

    // Load access file key pair.
    const keyPair = await AFC.getDerivedKeyPair(seed, AF_SYMBOL, AF_ACCOUNT_INDEX);

    // Extend settings.
    settings.accessfile.seed = seed;
    settings.accessfile.keyPair = keyPair;
}
