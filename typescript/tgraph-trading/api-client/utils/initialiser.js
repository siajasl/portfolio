/**
 * @fileOverview Encapsulates initialisation processes.
 */

import { Options as options } from './options';
import * as logger from './logging';

/**
 * Initialises client.
 *
 * @param {object} keyPair - Access file key pair used to sign/verify.
 */
export default async (keyPair) => {
    // TODO assert key pair.
    options.AF_KEY_PAIR = keyPair;

    return keyPair;
};
