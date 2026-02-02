// Use esm to wrap module imports.
require = require("esm")(module);

const {
    trading : {
        BOOK_SIDES,
        MATCHING_TYPES,
        ORDER_FILL_PREFERENCES,
        ORDER_STATES,
        ORDER_TYPES,
        TRADE_STATES,
        MatchingTypeEnum,
        BookSideEnum,
        OrderFillPreferenceEnum,
        OrderStateEnum,
        OrderTypeEnum,
        TradeStateEnum
    }
} = require('@trinkler/trade-constants');

export {
    BOOK_SIDES,
    MATCHING_TYPES,
    ORDER_FILL_PREFERENCES,
    ORDER_STATES,
    ORDER_TYPES,
    TRADE_STATES,
    MatchingTypeEnum,
    BookSideEnum,
    OrderFillPreferenceEnum,
    OrderStateEnum,
    OrderTypeEnum,
    TradeStateEnum
}
