/**
 * @fileOverview Types of coins supported by wallet.
 */

import * as BTC from './btc';
import * as ETH from './eth';
import * as AF from './af';
import * as NIM from './nim';

// Set of supported coins.
export const SUPPORTED = [
    BTC,
    ETH,
    AF,
    NIM
];

/**
 * Returns coin metadata mapping to passed chain identifier.
 *
 * @param {string} symbol - Coin symbol.
 */
export const getBySymbol = (symbol) => {
    symbol = symbol.toUpperCase();

    return SUPPORTED.find(i => i.symbol === symbol);
};

export {
    BTC,
    ETH,
    AF,
    NIM
}
