/**
 * @fileOverview Registers a settlement secret with server to complete settlement.
 */

import { execMutation, getEnvelope, mapString } from '../utils/gql';

// Action being executed.
const ACTION = 'registerSettlementSecret';

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
        channelID:           ${mapString(data.channelID)}
        redeemScript:        ${mapString(data.redeemScript)}
        refundTransaction:   ${mapString(data.refundTransaction)}
        settlementID:        ${mapString(data.settlementID)}
    }`;
};
