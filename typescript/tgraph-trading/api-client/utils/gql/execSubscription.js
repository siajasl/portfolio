/**
 * @fileOverview GQL subscription executor.
 */

import gql from 'graphql-tag';
import * as logger from '../logging';
import getClient from './clientFactory';
import { mapResponse } from './mappers';

/**
 * Returns response data from a GQL subscription execution.
 * @param {String} subscription - GraphQL subscription string.
 * @param {string} path - GQL endpoint path, e.g. kyc.getIndividualCustomer.
 * @param {Object} variables - Map of String <-> Any to be passed to GQL server.
 * @param {EventEmitter} eventEmitter - Object broadcasting events upstream.
 * @return {Object} Query subscription returned from remote server.
 */
export default async (subscription, path, variables, eventEmitter) => {
    logger.logInfo(`${path} :: listening to GQL subscription`);

    // Setup GQL subscription.
    const client = getClient();
    const observable = client.subscribe({
        query: gql(subscription),
        variables: variables || {}
    });

    // Await events.
    observable.subscribe({
        next: (response) => {
            eventEmitter.emit(path, mapResponse(response, path));
        },
        error: (err) => {
            eventEmitter.emit(`${path}:error`, err);
        }
    });
};
