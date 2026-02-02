/**
 * @fileOverview Package entry point.
 */
import * as clearing from './clearing/index';
import * as shared from './shared/index';
import * as trading from './trading/index';

// Library version.
export const NAME = 'Trade Telegraph - Trading & Clearing Domain Model';

// Library provider.
export const PROVIDER = 'Trinkler Software AG';

// Library version.
export const VERSION = '0.7.3';

export {
    clearing,
    shared,
    trading,
}
