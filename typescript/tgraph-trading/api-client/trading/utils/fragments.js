/**
 * @fileOverview GQL fragments used across queries.
 */

export const orderFields = `
    fragment orderFields on Order {
        addressOfBaseAsset
        addressOfQuoteAsset
        assetPair
        customerID
        customerReferenceID
        exchange
        exchangeID
        orderID
        price
        quantity
        quantityFilled
        quoteID
        quoteTimestamp
        side
        state
        timestamp
        type
    }
`;

export const tradeInfoFields = `
    fragment tradeInfoFields on TradeInfo {
        price
        quantity
        tradeID
        settlementID
        state
        timestamp
    }
`;
