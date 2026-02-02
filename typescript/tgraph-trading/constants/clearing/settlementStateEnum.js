/**
 * @fileOverview The set of states through which a settlement may pass.
 */

// Enumeration of settlement states.
export const SettlementStateEnum = Object.freeze({
    // Initial state - assigned by clearing engine.
    New: 'New',

    // Channel one: setup: awaiting.
    InitiateSetupAwaiting: 'InitiateSetupAwaiting',

    // Channel one: setup: timeout (termination state).
    InitiateSetupTimeout: 'InitiateSetupTimeout',

    // Channel one: setup: established.
    InitiateSetupDone: 'InitiateSetupDone',


    // Channel one: redeem: awaiting.
    InitiateRedeemAwaiting: 'InitiateRedeemAwaiting',

    // Channel one: redeem: timeout (termination state).
    InitiateRedeemTimeout: 'InitiateRedeemTimeout',

    // Channel one: redeem: established.
    InitiateRedeemDone: 'InitiateRedeemDone',


    // Channel one: refund: awaiting.
    InitiateRefundAwaiting: 'InitiateRefundAwaiting',

    // Channel one: refund: success (termination state).
    InitiateRefundSuccess: 'InitiateRefundSuccess',

    // Channel one: refund: error (termination state).
    InitiateRefundError: 'InitiateRefundError',


    // Channel two: setup: awaiting.
    ParticipateSetupAwaiting: 'ParticipateSetupAwaiting',

    // Channel two: setup: timeout (termination state).
    ParticipateSetupTimeout: 'ParticipateSetupTimeout',

    // Channel two: setup: done.
    ParticipateSetupDone: 'ParticipateSetupDone',


    // Channel two: redeem: awaiting.
    ParticipateRedeemAwaiting: 'ParticipateRedeemAwaiting',

    // Channel two: redeem: timeout (termination state).
    ParticipateRedeemTimeout: 'ParticipateRedeemTimeout',

    // Channel two: redeem: established.
    ParticipateRedeemDone: 'ParticipateRedeemDone',


    // Channel two: refund: awaiting.
    ParticipateRefundAwaiting: 'ParticipateRefundAwaiting',

    // Channel two: refund: success (termination state).
    ParticipateRefundSuccess: 'ParticipateRefundSuccess',

    // Channel two: refund: error (termination state).
    ParticipateRefundError: 'ParticipateRefundError',

});
