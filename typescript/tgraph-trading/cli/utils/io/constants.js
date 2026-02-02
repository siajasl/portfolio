/**
 * @fileOverview I/O related constants.
 */

import * as fs from 'fs';
import * as os from 'os';

// Directory: Application home directory.
export const DIR_HOME = `${os.homedir()}/.tradetelegraph`;

// Directory: Clearing releated sub-directory.
export const DIR_CLEARING = `${DIR_HOME}/clearing`;

// Directory: Clearing open settlements releated sub-directory.
export const DIR_CLEARING_OPEN = `${DIR_CLEARING}/open`;

// Directory: Clearing open settlements releated sub-directory.
export const DIR_CLEARING_PROCESSED = `${DIR_CLEARING}/processed`;

// Directory: Clearing open settlements releated sub-directory.
export const DIR_CLEARING_ERROR = `${DIR_CLEARING}/error`;

// Directory: Clearing open settlements releated sub-directory.
export const DIR_CLEARING_ERROR_TIMEOUT = `${DIR_CLEARING}/error-timeout`;

// Directory: Trading releated sub-directory.
export const DIR_TRADING = `${DIR_HOME}/trading`;

// Directory: All.
export const DIR_ALL = [
    DIR_HOME,
    DIR_CLEARING,
    DIR_CLEARING_OPEN,
    DIR_CLEARING_PROCESSED,
    DIR_CLEARING_ERROR,
    DIR_CLEARING_ERROR_TIMEOUT,
    DIR_TRADING
];

// File: Settings.
export const FILE_SETTINGS = `${DIR_HOME}/settings.json`;
