/**
 * @fileOverview Graphql client wrapper.
 */

import WebSocket from 'ws';
import { ApolloClient } from 'apollo-client';
import { ApolloLink } from 'apollo-link';
import { HttpLink } from 'apollo-link-http';
import { InMemoryCache } from 'apollo-cache-inmemory';
import { WebSocketLink } from 'apollo-link-ws';
import { getMainDefinition } from 'apollo-utilities';
import { onError } from 'apollo-link-error';
import { split } from 'apollo-link';
import apolloLogger from 'apollo-link-logger';
import fetch from 'node-fetch/lib/index';
import * as logger from '../logging';
import { name, version } from '../../index';
import { Options as options } from '../options';

// Singleton instance.
let instance = null;

/**
 * Returns an Appollo client instance.
 */
export default () => {
    if (instance === null) {
        instance = getInstance();
    }

    return instance;
};

/**
 * Returns instance of gql client.
 */
const getInstance = () => {
    return new ApolloClient({
        link: ApolloLink.from([
            onError(onLinkError),
            getTerminatingLink()
        ]),
        name,
        version,
        cache: new InMemoryCache(),
        connectToDevTools: true,
    });
}

/**
 * Returns multi-protocol directional composition Apollo link object.
 */
const getTerminatingLink = () => {
    return split(
        hasSubscriptionOperation,
        getLinkForWebSocket(),
        getLinkForHTTP(),
    )
};

/**
 * Returns flag indicating whether operation being processed is a subscription.
 */
const hasSubscriptionOperation = ({ query }) => {
    const { kind, operation } = getMainDefinition(query);

    return kind === 'OperationDefinition' && operation === 'subscription';
}

/**
 * Returns http channel Apollo link object.
 */
const getLinkForHTTP = () => {
    const opts = {
        uri: options.API_HTTP_ENDPOINT,
        options: {
            credentials: 'same-origin',
            reconnect: true,
        },
    };
    if (process.browser === undefined) {
        opts['fetch'] = fetch;
    }

    return new HttpLink(opts);
};

/**
 * Returns web-socket channel Apollo link object.
 */
const getLinkForWebSocket = () => {
    return new WebSocketLink({
        uri: options.API_WS_ENDPOINT,
        options: {
            reconnect: true
        },
        webSocketImpl: process.browser ? null : WebSocket
    });
};

/**
 * Link error handler.
 */
const onLinkError = ({ graphQLErrors, networkError }) => {
    if (graphQLErrors) {
        graphQLErrors.map(({ message, locations, path }) => {
            logger.logErr(`[GraphQL error]: Message: ${message}, Location: ${locations}, Path: ${path}`);
        });
    }
    if (networkError) {
        logger.logErr(`[Network error]: ${networkError}`);
    };
};
