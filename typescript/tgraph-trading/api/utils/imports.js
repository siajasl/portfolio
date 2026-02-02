/**
 * @fileOverview Wraps imports of various libs so as to ensure that they
 *               are importable in node environment.
 */

import WebSocket from 'ws';

// Use esm to wrap module imports.
require = require("esm")(module);

// Export access file (core) for internal use.
const AFC = require('@trinkler/accessfile-core');

// Export DLT transaction listening for internal use.
global.WebSocket = WebSocket;
const IW_TX = require('@trinkler/accessfile-tx');

// Export trading/clearing engines.
const { trading: TradingEngine, clearing: ClearingEngine } = require('@trinkler/trade-domain');

// Export domain constants.
const CONSTANTS = require('@trinkler/trade-constants');

export {
    AFC,
    IW_TX,
    CONSTANTS,
    TradingEngine,
    ClearingEngine
}
