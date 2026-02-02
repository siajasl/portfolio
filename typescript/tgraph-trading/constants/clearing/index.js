/**
 * @fileOverview Sub-package entry-point.
 */

import { ChannelTypeEnum } from './channelTypeEnum';
import { SettlementStateEnum } from './settlementStateEnum';
import { TransactionStateEnum } from './transactionStateEnum';
import { TransactionTypeEnum } from './transactionTypeEnum';

// Convert enums to simple lists as these can be useful in certain scenarios.
const CHANNEL_TYPES = Object.keys(ChannelTypeEnum);
const SETTLEMENT_STATES = Object.keys(SettlementStateEnum);
const TRANSACTION_STATES = Object.keys(TransactionStateEnum);
const TRANSACTION_TYPES = Object.keys(TransactionTypeEnum);

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
