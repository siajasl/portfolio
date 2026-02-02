/**
* @fileOverview Maps a GQL layer struct to a trading enging layer struct.
 */

import { TradingEngine } from '../../utils/imports';

/**
 * Maps a trading engine layer struct to a dB layer struct.
 */
export default (input, exchange) => {
    // Destructure GQL struct.
    const {
        addressOfBaseAsset,
        addressOfQuoteAsset,
        assetPair: symbol,
        customerID,
        customerReferenceID,
        options,
        price,
        quantity,
        side,
        type
    } = input;

    // Destructure exchange struct.
    const {
        exchangeID,
        symbol: exchangeSymbol
    } = exchange;

    // Return domain model instance.
    return new TradingEngine.Quote({
        addressOfBaseAsset,
        addressOfQuoteAsset,
        assetPair: exchange.getAssetPair(symbol),
        customerID,
        customerReferenceID,
        exchangeID,
        exchangeSymbol,
        options: new TradingEngine.QuoteOptions(options),
        price,
        quantity,
        side,
        type,
    });
};
