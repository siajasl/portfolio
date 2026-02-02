/**
* @fileOverview Maps a GQL layer struct to a domain layer struct.
 */

import { TradingEngine } from '../../utils/imports';

/**
 * Maps a GQL layer struct to a domain layer struct.
 */
export default (orderDocument, assetPair) => {
    const quote = new TradingEngine.Quote(Object.assign(orderDocument._doc, {
        assetPair,
        options: new TradingEngine.QuoteOptions(orderDocument.options)
    }));

    return new TradingEngine.Order(Object.assign(orderDocument._doc, {
        filled: orderDocument.quantityFilled,
        quote
    }));
};
