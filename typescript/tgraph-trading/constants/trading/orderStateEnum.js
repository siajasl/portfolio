/**
 * @fileOverview Set of order states.
 */

// Enumeration of order state.
export const OrderStateEnum = Object.freeze({
    // Initial state (prior to submitting to an order book).
    NEW: 'NEW',

    // Cancelled - termination state.
    CANCELLED: 'CANCELLED',

    // Expired - termination state (N.B. unused at present).
    EXPIRED: 'EXPIRED',

    // Totally filled - termination state.
    FILLED: 'FILLED',

    // Partially filled - still open.
    PARTIALLY_FILLED: 'PARTIALLY_FILLED',

    // Rejected - termination state.
    REJECTED: 'REJECTED',

    // Unfilled (however has been submitted to a book).
    UNFILLED: 'UNFILLED',
});
