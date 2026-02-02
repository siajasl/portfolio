/**
 * @fileOverview Wraps imports of various libs so as to ensure that they
 *               are importable in node, test & brwoser environments.
 */

// When in node context use esm to wrap module imports.
const isNode = require('detect-node');
if (isNode) {
    require = require("esm")(module);
}

// Export access file (core).
export const AFC = require('@trinkler/accessfile-core');

// Export constants.
export const CONSTANTS = require('@trinkler/trade-constants');
