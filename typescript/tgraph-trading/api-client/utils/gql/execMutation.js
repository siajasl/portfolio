/**
 * @fileOverview GQL mutation executor.
 */

import gql from 'graphql-tag';
import getClient from './clientFactory';
import * as logger from '../logging';
import { mapResponse } from './mappers';

/**
 * Returns response data from a GQL mutation execution.
 * @param {String} qry - GraphQL mutation to be executed.
 * @param {string} path - GQL endpoint path, e.g. kyc.getIndividualCustomer.
 * @return {Object} Mutation response returned from remote server.
 */
export default async (mutation, path) => {
    logger.logInfo(`${path} :: executing GQL mutation`);
    const client = getClient();

    let response;
    try {
        response = await client.mutate({
            mutation: gql(mutation),
        });
    } catch (err) {
        console.log(err);
        throw err;
    }

    return mapResponse(response, path);
};
