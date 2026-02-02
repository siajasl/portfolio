/**
* @fileOverview Mnemonic code for generating deterministic keys
 */

import { isBuffer, isString } from 'lodash';
import { entropyToMnemonic, mnemonicToSeed, validateMnemonic } from 'bip39';
import {
    MNEMONIC_SEPERATOR,
    VALID_ENTROPY_LENGTH,
    VALID_MNEMONIC_LENGTH,
    VALID_SEED_LENGTH
} from '../utils/constants';

/**
 * Returns a 24 word menmonic string derived from supplied entropy.
 * @param {Buffer} entropy - 32 bytes of entropy derived from a PRNG.
 * @return {String} A 24 word menmonic string.
 */
export const getMnemonic = async (source) => {
    // Map from 32 bytes of entropy.
    if (isEntropy(source)) {
        return await entropyToMnemonic(source);
    }

    throw new TypeError('Cannot map entropy to a menemonic');
};

/**
 * Returns a seed mapped from entropy or mnemonic.
 * @param {Buffer|String} source - entropy or mnemonic.
 * @return {Buffer} Seed mapped from entropy.
 */
export const getSeed = async (source) => {
    // 64 byte buffer is a valid seed.
    if (isSeed(source)) {
        return source;
    }

    // Map from 32 bytes of entropy.
    if (isEntropy(source)) {
        return await entropyToSeed(source);
    }

    // Map from 24 word mnemonic.
    if (isMnemonic(source)) {
        return await mnemonicToSeed(source);
    }

    throw new TypeError('Cannot map source to a seed');
};

/**
 * Returns flag indicating whether passed value is a valid entropy.
 */
const isEntropy = (source) => {
    return isBuffer(source) && source.length === VALID_ENTROPY_LENGTH;
}

/**
 * Returns flag indicating whether passed value is a valid entropy.
 */
const isMnemonic = (source) => {
    return validateMnemonic(source) &&
           source.split(MNEMONIC_SEPERATOR).length === VALID_MNEMONIC_LENGTH;
}

/**
 * Returns flag indicating whether passed value is a valid seed.
 */
const isSeed = (source) => {
    return isBuffer(source) && source.length === VALID_SEED_LENGTH;
}

/**
 * Returns a seed mapped from supplied entropy.
 */
const entropyToSeed = async (entropy) => {
    // TODO: support password ?
    // TODO: support wordlist selection ?
    const m = await entropyToMnemonic(entropy);

    return await mnemonicToSeed(m);
};
