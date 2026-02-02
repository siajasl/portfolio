/**
 * @fileOverview Retrieves order from dB.
 */

import compose from '../shared/resolvers/factory';
import mapDocumentToOutput from '../../mappers/trading/mongoToGql_order';

/**
 * Returns settlement information from dB.
 */
const resolve = async (parent, { input, envelope }, { dbe }, info) => {
    // Pull.
    const orderDocument = await dbe.getOrder(input);
    if (!orderDocument) {
        throw new Error('Either order does not exist or access denied.');
    }
    const tradeDocuments = await dbe.getTradesByOrder(input);

    // Map.
    return mapDocumentToOutput(orderDocument, tradeDocuments);
};

// Export composed resolver.
export default compose(resolve, 'getting order');
