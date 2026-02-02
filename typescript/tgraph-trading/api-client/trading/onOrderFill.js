/**
 * @fileOverview Listens to onOrderFill events streaming from remote server.
 */

import { execSubscription, getEnvelope, mapString } from '../utils/gql/index';
import { eventEmitter } from './utils/events';

// Action being executed.
const ACTION = 'onOrderFill';

/**
 * Subscribes to an event published by API.
 */
export default async data => await execSubscription(getGql(data), ACTION, null, eventEmitter);

/**
 * Returns GQL instruction to be process by server.
 */
const getGql = (data) => {
    return `
        subscription {
            ${ACTION} (
                input: ${getPayload(data)}
                envelope: ${getEnvelope(data, ACTION)}
            ) {
                customerReferenceID
                orderID
                settlementID
                tradeID
            }
        }
    `;
};

/**
 * Returns GQL payload.
 */
const getPayload = (data) => {
    return `{
        customerID: ${mapString(data.customerID)}
    }`;
};
