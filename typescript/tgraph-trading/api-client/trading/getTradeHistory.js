/**
 * @fileOverview Returns live order book information.
 */

import { execQuery, getEnvelope, mapDate, mapString } from '../utils/gql/index';

// Action being executed.
const ACTION = 'getTradeHistory';

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
                customerID
                exchange
                orderID
                price
                quantity
                settlementID
                side
                state
                tradeID
                timestamp
            }
        }
    `;
};

/**
 * Returns GQL payload.
 */
const getPayload = (data) => {
    if (data.assetPair && data.state) {
        return `{
            assetPair:  ${data.assetPair}
            customerID: ${mapString(data.customerID)}
            dateFrom:   ${mapDate(data.dateFrom)}
            dateTo:     ${mapDate(data.dateTo)}
            state:      ${data.state}
        }`;
    } else if (data.assetPair) {
        return `{
            assetPair:  ${data.assetPair}
            customerID: ${mapString(data.customerID)}
            dateFrom:   ${mapDate(data.dateFrom)}
            dateTo:     ${mapDate(data.dateTo)}
        }`;
    } else if (data.state) {
        return `{
            customerID: ${mapString(data.customerID)}
            dateFrom:   ${mapDate(data.dateFrom)}
            dateTo:     ${mapDate(data.dateTo)}
            state:      ${data.state}
        }`;
    } else {
        return `{
            customerID: ${mapString(data.customerID)}
            dateFrom:   ${mapDate(data.dateFrom)}
            dateTo:     ${mapDate(data.dateTo)}
        }`;
    }
};
