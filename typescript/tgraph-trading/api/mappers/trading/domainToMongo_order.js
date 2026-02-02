/**
 * @fileOverview Maps a trading engine layer struct to a dB layer struct.
 */

/**
 * Maps a trading engine layer struct to a dB layer struct.
 */
export default (order) => {
    // Destructure domain objects.
    const {
        assetPair: {
            symbol: assetPair
        },
        customerID,
        filled: quantityFilled,
        orderID,
        quote,
        state,
        timestamp,
    } = order;
    const {
        addressOfBaseAsset,
        addressOfQuoteAsset,
        customerReferenceID,
        exchangeID,
        exchangeSymbol,
        options,
        price,
        quantity,
        quoteID,
        side,
        type,
        timestamp: quoteTimestamp,
    } = quote;

    // Return dB struct.
    return {
        addressOfBaseAsset,
        addressOfQuoteAsset,
        assetPair,
        customerID,
        customerReferenceID,
        exchangeID,
        exchangeSymbol,
        options,
        orderID,
        price: price.isNaN() ? null : price,
        quantity,
        quantityFilled,
        quoteID,
        quoteTimestamp,
        side,
        state,
        type,
        timestamp,
    };
};

/**
 * Maps a trading engine layer struct to a dB layer struct.
 */
const mapQuote = (quote) => {
    // Destructure trading engine domain model instance.
    const {
        assetPair: {
            symbol: assetPair
        },
        addressOfBaseAsset,
        addressOfQuoteAsset,
        customerReferenceID,
        customerID,
        exchangeID,
        options,
        price,
        quantity,
        quoteID,
        side,
        type,
        timestamp,
    } = quote;

    // Return dB struct.
    return {
        assetPair,
        addressOfBaseAsset,
        addressOfQuoteAsset,
        customerReferenceID,
        customerID,
        exchangeID,
        options,
        price: price.isNaN() ? null : price,
        quantity,
        quoteID,
        side,
        type,
        timestamp,
    };
};
