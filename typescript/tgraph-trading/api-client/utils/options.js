/**
 * @fileOverview Cross package configurable options.
 */
import * as crypto from './crypto';

// We will export these options as static properties so that
// they can be consistently overridden.
export class Options {};

// Identifier used to correlate requests during processing.
Options.CORRELATION_ID = crypto.getRandomString(8);

// URI of the GraphQL http endpoint.
Options.API_HTTP_ENDPOINT = process.env.TRADE_API_HTTP_ENDPOINT || 'https://alpha-api.tradetelegraph.com';

// URI of the GraphQL web-socket endpoint.
Options.API_WS_ENDPOINT = process.env.TRADE_API_WS_ENDPOINT || 'wss://alpha-api.tradetelegraph.com';

// Access file key pair.
Options.AF_KEY_PAIR = null;
