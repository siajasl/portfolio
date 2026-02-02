/**
 * @fileOverview Wraps various enum imports from domain model.
 */

import { CONSTANTS } from './imports';

// Destructure from the domain model.
const {
    clearing: {
        CHANNEL_TYPES,
        SETTLEMENT_STATES,
        TRANSACTION_STATES,
        TRANSACTION_TYPES,
        ChannelTypeEnum,
        SettlementStateEnum,
        TransactionStateEnum,
        TransactionTypeEnum
    },
    trading: {
        ORDER_BOOK_SIDES,
        ORDER_FILL_PREFERENCES,
        ORDER_STATES,
        ORDER_TYPES,
        TRADE_STATES,
        BookSideEnum,
        OrderFillPreferenceEnum,
        OrderStateEnum,
        OrderTypeEnum,
        TradeStateEnum
    }
} = CONSTANTS;

/**
 * An enumeration over supported order book symbols.
 */
const AssetPairEnum = Object.freeze({
    ETHBTC: 'ETHBTC',
});

// Exported as a simple list.
const ASSET_PAIRS = Object.keys(AssetPairEnum);

/**
 * An enumeration over supported asset symbols.
 */
const AssetTypeEnum = Object.freeze({
    BTC: 'BTC',
    ETH: 'ETH',
});

// Exported as a simple list.
const ASSET_TYPES = Object.keys(AssetTypeEnum);

/**
 * An enumeration over supported exchange descriptions.
 */
const ExchangeDescriptionEnum = Object.freeze({
    // Standard exchange.
    TT01: 'Standard Limit/Market',

    // OTC desk.
    TT02: 'OTC Desk',

    // Merchant desk.
    TT03: 'Merchant Payments',
});

/**
 * An enumeration over supported exchange types.
 */
const ExchangeTypeEnum = Object.freeze({
    // Standard exchange.
    TT01: 'TT01',

    // OTC desk.
    TT02: 'TT02',

    // Merchant desk.
    TT03: 'TT03',
});

export {
    // clearing enums
    CHANNEL_TYPES,
    SETTLEMENT_STATES,
    TRANSACTION_STATES,
    TRANSACTION_TYPES,
    ChannelTypeEnum,
    SettlementStateEnum,
    TransactionStateEnum,
    TransactionTypeEnum,

    // shared enums
    ASSET_PAIRS,
    ASSET_TYPES,
    AssetPairEnum,
    AssetTypeEnum,

    // trading enums
    ORDER_BOOK_SIDES,
    ORDER_FILL_PREFERENCES,
    ORDER_STATES,
    ORDER_TYPES,
    TRADE_STATES,
    ExchangeDescriptionEnum,
    ExchangeTypeEnum,
    BookSideEnum,
    OrderFillPreferenceEnum,
    OrderStateEnum,
    OrderTypeEnum,
    TradeStateEnum,
}
