/**
 * @fileOverview API entry point.
 */

import { formatError } from 'apollo-errors';
import { GraphQLServer, PubSub } from 'graphql-yoga';
import { importSchema } from 'graphql-import'
import * as constants from './utils/constants';
import * as logger from './utils/logging';
import * as dbEngine from './db/engine';
import * as dltHooks from './dlt-hooks/index';
import * as smtp from './smtp/index';
import resolvers from './gql-resolvers/index';
import spinup from './ops/spinup/index';
import setDltListeners from './dlt-hooks/setListeners';
import { ClearingEngine, TradingEngine } from './utils/imports';

// API version.
const VERSION = '0.12.1';

// Server instance.
let instance;

/**
 * Factory: returns server options.
 */
const getOptions = () => {
    return {
        formatError,
        port: constants.GQL_PORT,
        subscriptions: {
            path: '/'
        }
    };
};

/**
 * Factory: returns server context.
 * @param {Array} exchanges - Supported trading exchanges.
 */
const getContext = async (exchanges) => {
    return {
        clearing: ClearingEngine,
        exchanges: exchanges,
        pubsub: new PubSub(),
        smtp,
        trading: TradingEngine,
    };
};

/**
 * Factory: returns server instance to be launched.
 * @param {Array} exchanges - Supported trading exchanges.
 */
const getInstance = async (exchanges) => {
    const context = await getContext(exchanges);

    return new GraphQLServer({
        typeDefs: await importSchema(`${__dirname}/gql-schema/main.graphql`),
        resolvers,
        context
    });
}

/**
 * Starts a server instance.
 */
export const start = async () => {
    // Enforce singleton pattern.
    if (instance) {
        return;
    }

    // Set db engine.
    const dbe  = await dbEngine.initialise('mutation');

    // Invoke spin up tasks.
    const exchanges = await spinup(dbe);

    // Set server.
    instance = await getInstance(exchanges);

    // Launch server.
    const opts = getOptions();
    await instance.start(opts, async () => {
        logger.logInfo(`accepting requests :: v${VERSION} @ http://localhost:${opts.port}`);
    });

    // Set dlt listeners.
    await setDltListeners(dbe, instance.context.pubsub);
};
