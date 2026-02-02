/**
 * @fileOverview Rejects individual customer level 1 registration.
 */

import { execQuery, getEnvelope } from '../utils/gql/index';
import { GetIdentityVerificationStatus as INPUT } from './inputs/index';

// Action being executed.
const ACTION = 'getIdentityVerificationStatus';

/**
 * Mutates state upon the remote server.
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
                input: ${INPUT.toGql(data)}
                envelope: ${getEnvelope(data, ACTION)}
            )
            {
                rejectionReason
                status
            }
        }
    `;
};
