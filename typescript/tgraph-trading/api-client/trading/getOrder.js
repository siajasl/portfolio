/**
 * @fileOverview Returns order information.
 */

import { execQuery, getEnvelope, mapString } from '../utils/gql/index';
import * as fragments from './utils/fragments';

// Action being executed.
const ACTION = 'getOrder';

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
            ${ACTION} (
                input: ${getPayload(data)}
                envelope: ${getEnvelope(data, ACTION)}
            ) {
                ...orderFields
                trades {
                    ...tradeInfoFields
                }
            }
        }

        # Fragments reused across query.
        ${fragments.orderFields}
        ${fragments.tradeInfoFields}
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
