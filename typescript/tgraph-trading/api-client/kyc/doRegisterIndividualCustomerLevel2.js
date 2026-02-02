/**
 * @fileOverview Pushes level 2 customer registration information to remote server.
 */

import { execMutation, getEnvelope } from '../utils/gql/index';
import { IndividualCustomerLevel2 as INPUT } from './inputs/index';

// Action being executed.
const ACTION = 'registerIndividualCustomerLevel2';

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
