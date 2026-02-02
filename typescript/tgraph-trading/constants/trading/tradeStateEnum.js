/**
 * @fileOverview Set of trade states through which a trade may pass through.
 */

// Enumeration of trade states.
export const TradeStateEnum = Object.freeze({
    // Initial state - assigned by trading engine.
    MATCHED: 'MATCHED',

    // Settled - termination state.
    SETTLED: 'SETTLED',

    // In error due to settlement issue.
    SETTLEMENT_ERROR: 'SETTLEMENT_ERROR',
});
