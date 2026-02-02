/**
 * @fileOverview Adds an order to the order book.
 */

import compose from '../shared/resolvers/factory';

/**
 * Returns settlement information from dB.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    return await dbe.getSettlementList(input);
};

// Export composed resolver.
export default compose(resolve, 'getting settlement list');
