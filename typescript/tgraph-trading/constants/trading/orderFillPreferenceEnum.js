/**
 * @fileOverview Set of order fill preferences.
 */

// Enumeration over supported order fill preferences.
export const OrderFillPreferenceEnum = Object.freeze({
    // Customer wishes completely fulfillment otherwise take order is cancelled.
    ALL: 'ALL',

    // Customer accepts partially fulfillment.
    PARTIAL: 'PARTIAL',
});
