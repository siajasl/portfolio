/**
 * @fileOverview DLT hooks for channel refund events.
 */

/**
 * DLT event callback invoked upon processing of a channel refund tx upon a supported DLT.
 * @param {String} assetType - Type of asset being settled, e.g. ETH.
 * @param {Object} txDetails - Transaction details such as hash, status ...etc.
 */
export default async (dbe, pubsub, assetType, txDetails) => {
    console.log('TODO: process refund tx confirmation');
};
