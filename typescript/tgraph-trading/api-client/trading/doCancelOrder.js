/**
 * @fileOverview Pushes a remove order to remote server.
 */

import { execMutation, getEnvelope, mapString } from '../utils/gql/index';

// Action being executed.
const ACTION = 'cancelOrder';

/**
 * Mutates state upon the remote server.
 */
export default async data => await execMutation(getGql(data), ACTION);

/**
 * Returns GQL instruction to be process by server.
 */
const getGql = (data) => {
    return `
    mutation {
        ${ACTION} (
            input: ${getPayload(data)}
            envelope: ${getEnvelope(data, ACTION)}
        )
    }
    `;
};

/**
 * Returns GQL payload.
 */
const getPayload = (data) => {
    return `{
        customerID: ${mapString(data.customerID)}
        orderID:    ${mapString(data.orderID)}
    }`;
};
