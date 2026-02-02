/**
 * @fileOverview Submits an order (quote) to API for processing.
 */

import { execMutation, getEnvelope, mapObject, mapString } from '../utils/gql';

// Action being executed.
const ACTION = 'addOrder';

/**
 * Mutates state upon the remote server.
 */
export default async data => await execMutation(getGql(data), ACTION);

/**
 * Returns GQL instruction to be process by server.
 */
const getGql = (data) => {
    return `
    mutation {
        ${ACTION} (
            input: ${getPayload(data)}
            envelope: ${getEnvelope(data, ACTION)}
        ) {
            addressOfBaseAsset
            addressOfQuoteAsset
            assetPair
            customerReferenceID
            exchange
            orderID
            price
            quantity
            quantityFilled
            quoteID
            quoteTimestamp
            side
            state
            timestamp
            trades {
                price
                quantity
                tradeID
                settlementID
                state
                timestamp
            }
            type
        }
    }
    `;
};

/**
 * Returns GQL payload.
 */
const getPayload = (data) => {
    return `{
        addressOfBaseAsset:     ${mapString(data.addressOfBaseAsset)}
        addressOfQuoteAsset:    ${mapString(data.addressOfQuoteAsset)}
        assetPair:              ${data.assetPair}
        customerID:             ${mapString(data.customerID)}
        customerReferenceID:    ${mapString(data.customerReferenceID)}
        exchange:               ${data.exchange}
        options:                {
            fillPreference:     ${data.options.fillPreference}
            fillLowerBound:     ${mapString(data.options.fillLowerBound)}
            merchantID:         ${mapString(data.options.merchantID)}
            otcOrderID:         ${mapString(data.options.otcOrderID)}
        }
        price:                  ${mapString(data.price)}
        quantity:               ${mapString(data.quantity)}
        side:                   ${data.side}
        type:                   ${data.type}
    }`;
};
