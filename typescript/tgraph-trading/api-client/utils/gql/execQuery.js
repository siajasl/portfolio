/**
 * @fileOverview GQL query executor.
 */

import gql from 'graphql-tag';
import getClient from './clientFactory';
import * as logger from '../logging';
import { mapResponse } from './mappers';

/**
 * Returns response data from a GQL query execution.
 * @param {String} qry - GraphQL query string.
 * @param {string} path - GQL endpoint path, e.g. kyc.getIndividualCustomer.
 * @return {Object} Query response returned from remote server.
 */
export default async (qry, path) => {
    logger.logInfo(`${path} :: executing GQL query`);
    const client = getClient();
    const response = await client.query({
        query: gql(qry),
    });

    return mapResponse(response, path);
};
