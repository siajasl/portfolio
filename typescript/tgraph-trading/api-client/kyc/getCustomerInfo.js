/**
 * @fileOverview Pulls corporate customer information from GQL server.
 */

import { execQuery, getEnvelope, mapString } from '../utils/gql/index';

// Action being executed.
const ACTION = 'getCustomerInfo';

/**
 * Executes an API GQL query.
 */
export default async (data) => {
    const qry = getGql(data);

    return await execQuery(qry, ACTION);
};

/**
 * Returns GQL instruction to be process by server.
 */
const getGql = (data) => {
    return `
        query {
            ${ACTION}(
                input: {
                    customerID: ${mapString(data.customerID)}
                }
                envelope: ${getEnvelope(data, ACTION)}
            )
            {
                customerID
                type
                verificationLevel
                verificationStatus
                verificationTxReference
            }
        }
    `;
};
