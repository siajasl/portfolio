// Use esm to wrap module imports.
require = require("esm")(module);

const {
    clearing : {
        CHANNEL_TYPES,
        SETTLEMENT_STATES,
        TRANSACTION_STATES,
        TRANSACTION_TYPES,
        ChannelTypeEnum,
        SettlementStateEnum,
        TransactionStateEnum,
        TransactionTypeEnum
    }
} = require('@trinkler/trade-constants');

export {
    CHANNEL_TYPES,
    SETTLEMENT_STATES,
    TRANSACTION_STATES,
    TRANSACTION_TYPES,
    ChannelTypeEnum,
    SettlementStateEnum,
    TransactionStateEnum,
    TransactionTypeEnum
}
