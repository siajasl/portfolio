/**
 * @fileOverview Listens to onBookUpdate events streaming from remote server.
 */

import { execSubscription, getEnvelope, mapString } from '../utils/gql/index';
import { eventEmitter } from './utils/events';

// Action being executed.
const ACTION = 'onBookUpdate';

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
            assetPair
            asks {
              price
              quantity
              orders {
                  price
                  quantity
                  side
                  type
                  uid
              }
            }
            bids {
              price
              quantity
              orders {
                  price
                  quantity
                  side
                  type
                  uid
              }
            }
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
