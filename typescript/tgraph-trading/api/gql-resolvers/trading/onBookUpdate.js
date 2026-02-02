/**
 * @fileOverview Subscription pushed to a client application whenever an order book changes.
 */

import { withFilter } from 'graphql-yoga';
import * as logger from '../../utils/logging';
import { authenticateRequest } from '../shared/resolvers/authentication';

// Name of event to be published.
const EVENT_NAME = 'onBookUpdate';

/**
 * Processes event subscription.
 */
const subscribe = (parent, { input, envelope }, { pubsub }, info) => {
    authenticateRequest(input, envelope);
    logger.logInfo(`subscription : ${EVENT_NAME}`);

    return pubsub.asyncIterator(EVENT_NAME)
};

/**
 * Processes event publication.
 */
const publish = async (pubsub, { book }) => {
    logger.logInfo(`publishing : ${EVENT_NAME}`);
    await pubsub.publish(EVENT_NAME, {
        onBookUpdate: {
            assetPair: book.assetPair.symbol,
            exchange: book.exchange.symbol
        }
    });
};

export default {
    subscribe,
    publish
};
