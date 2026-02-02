/**
 * @fileOverview Adds an order to the order book.
 */

import * as _ from 'lodash';
import compose from '../shared/resolvers/factory';
import mapDocumentToOutput from '../../mappers/trading/mongoToGql_trade';

/**
 * Returns trade history information from dB.
 */
const resolve = async (parent, { input, envelope }, { dbe }, info) => {
    // Destructure.
    const { customerID } = input;

    // Query dB.
    const tradeDocuments = await dbe.getTradeHistory(input);

    // Map.
    return _.map(tradeDocuments, (t) => mapDocumentToOutput(customerID, t));
};

// Export composed resolver.
export default compose(resolve, 'getting trade history');
