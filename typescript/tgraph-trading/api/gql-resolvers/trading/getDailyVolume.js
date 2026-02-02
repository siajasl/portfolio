/**
 * @fileOverview Returns daily trade volume.
 */

import compose from '../shared/resolvers/factory';

/**
 * Returns daily trading volume over an exchange.
 */
const resolve = async (parent, { input, envelope }, { dbe }, info) => {
    return true;
};

// Export composed resolver.
export default compose(resolve, 'get daily trading volume');
