/**
 * @fileOverview Returns customer's open orders.
 */

import compose from '../shared/resolvers/factory';
import mapDocumentToOutput from '../../mappers/trading/mongoToGql_order';

/**
 * Returns a customer's set of open orders.
 */
const resolve = async (parent, { input, envelope }, { dbe }, info) => {
    // Pull.
    const orderDocuments = await dbe.getOpenOrders(input);

    // Map.
    return orderDocuments.map(i => mapDocumentToOutput(i));
};

// Export composed resolver.
export default compose(resolve, 'get customer open orders');
