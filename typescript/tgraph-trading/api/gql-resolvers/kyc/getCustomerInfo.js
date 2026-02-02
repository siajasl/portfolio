/**
 * @fileOverview Returns corporate customer information.
 */

import compose from '../shared/resolvers/factory';

/**
 * Returns corporate customer information from dB.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    return await dbe.getCustomerInfo(input.customerID);
};

// Export composed resolver.
export default compose(resolve, 'get customer info');
