/**
 * @fileOverview Package entry point.
 */

import * as clearing from './clearing/index';
import * as trading from './trading/index';

// Library version.
export const NAME = 'Trade Telegraph - Domain Constants';

// Library provider.
export const PROVIDER = 'Trinkler Software AG';

// Library version.
export const VERSION = '0.1.2';

export {
    clearing,
    trading,
}
