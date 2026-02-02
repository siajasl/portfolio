/**
 * @fileOverview Returns live order book information.
 */

import { execQuery, getEnvelope, mapString } from '../utils/gql/index';
import * as fragments from './utils/fragments';

// Action being executed.
const ACTION = 'getSettlement';

/**
 * Executes an API GQL query.
 */
export default async data => await execQuery(getGql(data), ACTION);

/**
 * Returns GQL instruction to be process by server.
 */
const getGql = (data) => {
    return `
        query {
            ${ACTION}(
                input: ${getPayload(data)}
                envelope: ${getEnvelope(data, ACTION)}
            )
            {
                assetPair
                counterPartyOneID
                counterPartyTwoID
                initiateChannel {
                    ...channelFields
                }
                participateChannel {
                    ...channelFields
                }
                settlementID
                state
                stateHistory {
                    state
                    timestamp
                }
                timestamp
            }
        }
        ${fragments.channelFields}
        ${fragments.transactionFields}
    `;
};

/**
 * Returns GQL payload.
 */
const getPayload = (data) => {
    return `{
        customerID:   ${mapString(data.customerID)}
        settlementID: ${mapString(data.settlementID)}
    }`;
};
