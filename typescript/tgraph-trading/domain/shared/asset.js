/**
 * @fileOverview An asset being traded.
 */

/**
 * An asset being traded.
 * @constructor
 * @param {string} symbol - Asset symbol, e.g. KTS.
 * @param {integer} decimals - Underlying DLT price unit decimals.
 */
export class Asset {
    constructor (symbol, decimals) {
        // Number of decimal places in fundamental price unit.
        this.decimals = decimals;

        // Asset symbol.
        this.symbol = symbol.toUpperCase();
    }

    /**
     * Returns amount by multiplying price & quantity & then rounding down to nearest decimal place.
     */
    getAmount(price, quantity) {
        return price.times(quantity).dp(this.decimals, BigNumber.ROUND_DOWN);
    }
}
