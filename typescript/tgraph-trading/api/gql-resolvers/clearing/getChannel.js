/**
 * @fileOverview Returns channel information from dB.
 */

import compose from '../shared/resolvers/factory';

/**
 * Returns channel information from dB.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    const settlement = await dbe.getSettlementByChannel(input);
    if (settlement) {
        const channel = settlement.getChannel(input.channelID);
        if (channel) {
            return channel;
        }
    }

    throw new Error('Either clearing channel does not exist or access denied to counter-party.');
};

// Export composed resolver.
export default compose(resolve, 'getting channel information');
