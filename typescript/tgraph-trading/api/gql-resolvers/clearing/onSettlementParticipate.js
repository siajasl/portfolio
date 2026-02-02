/**
 * @fileOverview Subscription pushed to counter party when settlement is participated.
 */

import { withFilter } from 'graphql-yoga';
import * as logger from '../../utils/logging';
import { authenticateRequest } from '../shared/resolvers/authentication';

// Name of event to be published.
const EVENT_NAME = 'onSettlementParticipate';

/**
 * Processes event subscription.
 */
const subscribe = withFilter(
    // Resolver.
    (parent, { input, envelope }, { pubsub }, info) => {
        authenticateRequest(input, envelope);
        logger.logInfo(`subscription : ${EVENT_NAME} : ${input.customerID}`);
        return pubsub.asyncIterator(EVENT_NAME)
    },

    // Predicate.
    (payload, { input }) => {
        const doPublish = payload.onSettlementParticipate.customerID === input.customerID;
        if (doPublish) {
            logger.logInfo(`publishing : ${EVENT_NAME} : ${input.customerID}`);
        }

        return doPublish;
    }
);

/**
 * Processes event publication.
 */
const publish = async (pubsub, data) => {
    await pubsub.publish(EVENT_NAME, {
        onSettlementParticipate:  data
    })
};

export default {
    subscribe,
    publish
};
