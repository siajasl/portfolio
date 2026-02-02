/**
 * @fileOverview Adds an order to the order book.
 */

import compose from '../shared/resolvers/factory';

/**
 * Returns settlement information from dB.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    const settlement = await dbe.getSettlement(input);
    if (settlement) {
        return settlement;
    }

    throw new Error('Either settlement does not exist or access denied to counter-party.');
};

// Export composed resolver.
export default compose(resolve, 'getting settlement');
