/**
 * @fileOverview Sub-package entry-point.
 */

import { BookSideEnum } from './bookSideEnum';
import { MatchingTypeEnum } from './matchingTypeEnum';
import { OrderFillPreferenceEnum } from './orderFillPreferenceEnum';
import { OrderStateEnum } from './orderStateEnum';
import { OrderTypeEnum } from './orderTypeEnum';
import { TradeStateEnum } from './tradeStateEnum';

// Convert enums to simple lists as these can be useful in certain scenarios.
const BOOK_SIDES = Object.keys(BookSideEnum);
const MATCHING_TYPES = Object.keys(MatchingTypeEnum);
const ORDER_FILL_PREFERENCES = Object.keys(OrderFillPreferenceEnum);
const ORDER_STATES = Object.keys(OrderStateEnum);
const ORDER_TYPES = Object.keys(OrderTypeEnum);
const TRADE_STATES = Object.keys(TradeStateEnum);

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
