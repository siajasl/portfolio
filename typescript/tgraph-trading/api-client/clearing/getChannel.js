/**
 * @fileOverview Returns clearing channel information.
 */

import { execQuery, getEnvelope, mapString } from '../utils/gql/index';
import * as fragments from './utils/fragments';

// Action being executed.
const ACTION = 'getChannel';

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
                ...channelFields
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
        customerID: ${mapString(data.customerID)}
        channelID:  ${mapString(data.channelID)}
    }`;
};
