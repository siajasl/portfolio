/**
 * @fileOverview Get the Jumio iframe url and the Tx Jumio Ref.
 */

import { execQuery, getEnvelope, mapString } from '../utils/gql/index';

// Action being executed.
const ACTION = 'getIdentityVerificationInitiationData';

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
                iframeUrl
                verificationTxReference
            }
        }
    `;
};
