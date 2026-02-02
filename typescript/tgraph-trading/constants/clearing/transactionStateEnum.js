/**
 * @fileOverview The set of states through which a transaction may pass.
 */

// Enumeration of DLT transaction states.
export const TransactionStateEnum = Object.freeze({
    // Tx state applicable when transaction has been instantiated.
    NEW: 'NEW',

    // Tx state applicable only to BTC redeem transactions.
    SCRIPT: 'SCRIPT',

    // Tx has been signed and is ready to submit.
    SIGNED: 'SIGNED',

    // Tx has been submitted to DLT but is currently in mempool awaiting processing.
    PENDING: 'PENDING',

    // Tx has been included in a block, i.e. it has been mined.
    MINED: 'MINED',

    // Tx has been mined since transaction has been confirmed N times (where N = customer's confirmation tolerance).
    CONFIRMED: 'CONFIRMED',
});
