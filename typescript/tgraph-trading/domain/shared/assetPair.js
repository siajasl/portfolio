/**
 * @fileOverview An asset pair being traded.
 */

import { BigNumber } from 'bignumber.js';

/**
 * An asset pair being traded.
 * @constructor
 * @param {Asset} base - The asset pair's base asset.
 * @param {Asset} quote - The asset pair's quote asset.
 */
export class AssetPair {
    constructor (base, quote) {
        this.base = base;
        this.quote = quote;
    }

    /**
     * Gets assets pair symbol.
     */
    get symbol() {
        return `${this.base.symbol}${this.quote.symbol}`;
    }

    /**
     * Gets total base amount being traded.
     */
    getBaseAmount(price, quantity) {
        return this.base.getAmount(price, quantity);
    }

    /**
     * Gets total quote amount being traded.
     */
    getQuoteAmount(price, quantity) {
        return this.quote.getAmount(price, quantity);
    }
}
