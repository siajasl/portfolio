/**
 * @fileOverview Maps a trading engine layer struct to a dB layer struct.
 */

/**
 * Maps a trading engine layer struct to a dB layer struct.
 */
export default (trade) => {
    // Destructure trading engine domain model instance.
    const {
        assetPair: {
            symbol: assetPair
        },
        exchangeID,
        exchangeSymbol,
        makeOrder: {
            customerID: makeCustomerID,
            orderID: makeOrderID,
            side: makeSide,
        },
        price,
        quantity,
        settlementID,
        state,
        takeOrder: {
            customerID: takeCustomerID,
            orderID: takeOrderID,
            side: takeSide,
        },
        timestamp,
        tradeID,
    } = trade;

    // Return dB struct.
    return {
        assetPair,
        exchangeID,
        exchangeSymbol,
        makeCustomerID,
        makeOrderID,
        makeSide,
        price,
        quantity,
        settlementID,
        state,
        stateHistory: [
            {
                state,
            }
        ],
        takeCustomerID,
        takeOrderID,
        takeSide,
        tradeID,
        timestamp
    };
};
