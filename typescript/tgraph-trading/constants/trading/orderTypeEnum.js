/**
 * @fileOverview Set of order types.
 */

// Enumeration of order types.
export const OrderTypeEnum = Object.freeze({
    // A limit order: price point is explicit.
    LIMIT: 'LIMIT',

    // A market order: price point is implicit.
    MARKET: 'MARKET',
});
