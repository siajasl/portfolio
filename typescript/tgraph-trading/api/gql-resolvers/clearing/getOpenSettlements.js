/**
 * @fileOverview Returns a customer's open settlements.
 */

import compose from '../shared/resolvers/factory';

/**
 * GQL endpoint resolver
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    return await dbe.getOpenSettlements(input);
};

// Export composed resolver.
export default compose(resolve, 'getting open settlements');
