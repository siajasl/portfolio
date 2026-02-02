/**
 * @fileOverview Registers a secret required to complete settlement.
 */

import compose from '../shared/resolvers/factory';

/**
 * Endpoint resolver.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    console.log("TODO: register settlement secret");

    // Add secret to db

    // Use trade-swap for delegatedRedeem (BTC)
    //  https://github.com/Trinkler/trade-swap/blob/master/src/btc/btc-htlc.ts#L493-L519

    // Use trade-swap for redeem (ETH)
    //  https://github.com/Trinkler/trade-swap/blob/master/src/eth/eth-htlc.ts#L151-L164

    // Add output transactionHashes of both of above transactions to db

    // Listen to tx.state using dlt listener (BTC)

    // Listen to tx.state using dlt listener (ETH)

    // Update db.settlementState as "RedeemAllAwaiting"
};

// Export composed resolver.
export default compose(resolve, 'registering settlement secret');
