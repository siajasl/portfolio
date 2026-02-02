/**
* @fileOverview Maps a trading enging layer struct to a GQL layer struct.
 */

/**
 * Maps trading engine outputs to GQL layer struct.
 */
export default (quote, order, trades) => {
    const {
        assetPair: {
            symbol: assetPair
        },
        exchangeSymbol: exchange,
        timestamp: quoteTimestamp
    } = quote;
    const {
        filled: quantityFilled,
        timestamp: orderTimestamp
    } = order;

    return {
        ...order,
        ...quote,
        assetPair,
        exchange,
        quantityFilled,
        quoteTimestamp,
        timestamp: orderTimestamp,
        trades
    }
};
