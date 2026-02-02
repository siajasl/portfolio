/**
 * @fileOverview Client interfae to remote API.
 */

import * as clearing from './clearing/index';
import * as kyc from './kyc/index';
import * as settlement from './clearing/index';
import * as trading from './trading/index';
import { Options as options } from './utils/options';
import * as utils from './utils/index';
import initialise from './utils/initialiser';

// Library version.
export const NAME = 'Trade Telegraph Javascript Client';

// Library provider.
export const PROVIDER = 'Trinkler Software AG';

// Library version.
export const VERSION = '0.11.1';

// ???
export const name = NAME;
export const version = VERSION;

// Module exports.
export {
    clearing,
    initialise,
    kyc,
    options,
    settlement,
    trading,
    utils,
};
