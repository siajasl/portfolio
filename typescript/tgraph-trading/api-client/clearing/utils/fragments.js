/**
 * @fileOverview GQL fragments used across queries.
 */

export const channelFields = `
    fragment channelFields on Channel {
        asset
        addressFrom
        addressTo
        amount
        channelID
        commission
        hashedSecret
        timeout
        timestamp
        txContract {
            ...transactionFields
        }
        txRedeem {
            ...transactionFields
        }
        txRefund {
            ...transactionFields
        }
        type
    }
`;

export const transactionFields = `
    fragment transactionFields on Transaction {
        hash
        script
        secret
        signature
        state
        stateHistory{
            state
            timestamp
        }
        timestamp
        transactionID
        type
    }
`;
