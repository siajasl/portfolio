/**
 * @fileOverview The set of supported transaction types.
 */

// Enumeration of transaction types.
export const TransactionTypeEnum = Object.freeze({
    // A channel's contract deployment transaction.
    CONTRACT: 'CONTRACT',

    // A channel's refund transaction.
    REFUND: 'REFUND',

    // A channel's redeem transaction.
    REDEEM: 'REDEEM',
});
