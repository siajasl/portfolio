/**
 * @fileOverview Returns live order book information.
 */

import { execQuery, getEnvelope } from '../utils/gql/index';

// Action being executed.
const ACTION = 'getBook';

/**
 * Executes an API GQL query.
 */
export default async data => await execQuery(getGql(data), ACTION);

/**
 * Returns GQL instruction to be process by server.
 */
const getGql = (data) => {
    return `
        query {
            ${ACTION} (
                input: ${getPayload(data)}
                envelope: ${getEnvelope(data, ACTION)}
            ) {
                assetPair
                asks {
                    price
                    quantity
                    orders {
                        price
                        quantity
                        side
                        type
                        uid
                    }
                }
                bids {
                    price
                    quantity
                    orders {
                        price
                        quantity
                        side
                        type
                        uid
                    }
                }
                exchange
            }
        }
    `;
};

/**
 * Returns GQL payload.
 */
const getPayload = (data) => {
    return `{
        assetPair: ${data.assetPair}
        askDepth:  ${data.askDepth}
        bidDepth:  ${data.bidDepth}
        exchange:  ${data.exchange}
    }`;
};
