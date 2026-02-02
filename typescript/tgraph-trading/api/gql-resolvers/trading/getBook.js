/**
 * @fileOverview Adds an order to the order book.
 */

import * as _ from 'lodash';
import compose from '../shared/resolvers/factory';
import mapBookToOutput from '../../mappers/trading/domainToGql_orderBook';

/**
 * Returns order book snapshot.
 */
const resolve = async (parent, { input }, { dbe, exchanges }, info) => {
    const exchange = exchanges.find(i => i.symbol === input.exchange);
    const book = exchange.getBook(input.assetPair);

    return mapBookToOutput(book);
};

// Export composed resolver.
export default compose(resolve, 'getting order book');
