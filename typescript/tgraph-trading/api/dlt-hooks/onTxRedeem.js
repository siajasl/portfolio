/**
 * @fileOverview DLT hooks for channel redemption events.
 */

/**
 * DLT event callback invoked upon processing of a channel redeem tx upon a supported DLT.
 * @param {String} assetType - Type of asset being settled, e.g. ETH.
 * @param {Object} txDetails - Transaction details such as hash, status ...etc.
 */
export default async (dbe, pubsub, assetType, txDetails) => {
    console.log('TODO: process redeem tx confirmation');
};
