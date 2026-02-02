/**
 * @fileOverview Pushes corporate registration information to remote server.
 */

import { execMutation, getEnvelope } from '../utils/gql/index';
import { CorporateCustomer as INPUT } from './inputs/index';

// Action being executed.
const ACTION = 'registerCorporateCustomer';

/**
 * Mutates state upon the remote server.
 */
export default async (data) => {
    const mutation = getGql(data);

    return await execMutation(mutation, ACTION);
};

/**
 * Returns GQL instruction to be process by server.
 */
const getGql = (data) => {
    return `
    mutation {
        ${ACTION}(
            input: ${INPUT.toGql(data)}
            envelope: ${getEnvelope(data, ACTION)}
        )
    }
    `;
};
