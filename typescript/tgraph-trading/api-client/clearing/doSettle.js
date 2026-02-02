/**
 * @fileOverview Signals intent by a counter-party to initiate a trade.
 */

import { execMutation, getEnvelope, mapString } from '../utils/gql';

// Action being executed.
const ACTION = 'settle';

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
        ${ACTION}(
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
        channelID:      ${mapString(data.channelID)}
        customerID:     ${mapString(data.customerID)}
        hashedSecret:   ${mapString(data.hashedSecret)}
        settlementID:   ${mapString(data.settlementID)}
        txContractHash: ${mapString(data.txContractHash)}
    }`;
};
