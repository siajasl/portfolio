/**
 * @fileOverview Application cache accessor.
 */

import * as settingsIO from './io/settings';

// Cached settings.json content.
let SETTINGS;

/**
 * Initialises cache.
 */
export const init = (settings) => {
    SETTINGS = settings;
}

/**
 * Returns an asset address.
 */
export const getAssetAddress = (symbol) => {
    return SETTINGS['assets'][symbol];
}

/**
 * Returns an asset address.
 */
export const getAssetAddresses = (symbol) => {
    if (symbol) {
        return {
            addressOfBaseAsset: getAssetAddress(symbol.slice(0, 3)),
            addressOfQuoteAsset: getAssetAddress(symbol.slice(3))
        };
    }
    return SETTINGS['assets'];
}

/**
 * Returns customer identifier.
 */
export const getCustomerID = () => {
    const pbk = getAccessFilePublicKey();

    return pbk.toString('hex');
}

/**
 * Returns access file settings.
 */
export const getAccessFile = () => {
    return SETTINGS.accessfile
}

/**
 * Returns access file private key.
 */
export const getAccessFilePrivateKey = () => {
    return SETTINGS.accessfile.keyPair.privateKey;
}

/**
 * Returns access file public key.
 */
export const getAccessFilePublicKey = () => {
    return SETTINGS.accessfile.keyPair.publicKey;
}

/**
 * Returns access file seed.
 */
export const getAccessFileSeed = () => {
    return SETTINGS.accessfile.seed;
}

/**
 * Returns a trading preference.
 */
export const getTradingPreference = (name) => {
    return SETTINGS['preferences'][name];
}

/**
 * Returns all trading preferences.
 */
export const getTradingPreferences = () => {
    return SETTINGS['preferences'];
}


/**
 * Sets path to customer's access file.
 */
export const setAccessFile = async (path) => {
    SETTINGS['accessfile']['path'] = path;
}

/**
 * Sets an asset address.
 */
export const setAssetAddress = async (symbol, address) => {
    SETTINGS['assets'][symbol] = address;
}

/**
 * Sets a trading preference.
 */
export const setTradingPreference = async (name, value) => {
    SETTINGS['preferences'][name] = value;
}

/**
 * Dumps cache contents to file system.
 */
export const dump = async () => {
    await settingsIO.write(SETTINGS);
}
