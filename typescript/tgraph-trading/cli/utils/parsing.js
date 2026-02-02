/**
 * @fileOverview Exposes parsing functions.
 */

const validateUUID = require('uuid-validate');
import { BigNumber } from 'bignumber.js';
import { DateTime } from 'luxon';
import {
    ASSET_PAIRS,
    ASSET_TYPES,
    ORDER_BOOK_SIDES,
    SETTLEMENT_STATES,
    TRADE_STATES
} from './enums';
import * as cache from './cache';

/**
 * Returns a parsed asset.
 */
export const parseAsset = (asset) => {
    return parseEnum(ASSET_TYPES, asset, 'symbol');
};

/**
 * Returns a parsed asset pair in readiness for further processing.
 */
export const parseAssetPair = (assetPair) => {
    return parseEnum(ASSET_PAIRS, assetPair, 'symbol');
};

/**
 * Parses channel information pulled from API.
 */
export const parseChannel = (channel) => {
    setBigNumber(channel, 'amount');
    setBigNumber(channel, 'commission');
    setISODateTime(channel, 'timestamp');
    setISODateTime(channel, 'timeout');
    parseTransaction(channel.txContract);
    parseTransaction(channel.txRedeem);
    parseTransaction(channel.txRefund);
};

/**
 * Returns a parsed enumeration item in readiness for dispatch to API.
 */
const parseEnum = (vals, val, fieldName) => {
    const whitelist = vals.map(i => i.toUpperCase());
    const idx = whitelist.indexOf(val.toUpperCase());
    if (idx === -1) {
        throw new Error(`Invalid ${fieldName}: ${val}`);
    }

    return vals[idx];
}

/**
 * Parses event data received from API.
 */
export const parseEventData = (eventData) => {
    setISODateTime(eventData, 'timestamp');
    eventData.eventType = eventData.__typename;
};

/**
 * Parses order information pulled from API.
 */
export const parseNewOrder = (order) => {
    setBigNumber(order, 'price');
    setBigNumber(order, 'quantity');
    setBigNumber(order, 'quantityFilled');
    setISODateTime(order, 'quoteTimestamp');
    setISODateTime(order, 'timestamp');
    order.trades.forEach(parseTrade);
};

/**
 * Parses order information pulled from API.
 */
export const parseOrder = (order) => {
    setBigNumber(order, 'filled');
    setBigNumber(order, 'price');
    setBigNumber(order, 'quantity');
    setBigNumber(order, 'quantityFilled');
    setISODateTime(order, 'quoteTimestamp');
    setISODateTime(order, 'timestamp');
    if (order.trades) {
        order.trades.forEach(parseTrade);
    }
};

/**
 * Parses order book information pulled from API.
 */
export const parseBook = (book) => {
    parseOrderTree(book.asks);
    parseOrderTree(book.bids);
};

/**
 * Returns a parsed order book side in readiness for dispatch to API.
 */
export const parseBookSide = (side) => {
    return parseEnum(ORDER_BOOK_SIDES, side, 'side');
};

/**
 * Parses order tree information pulled from API.
 */
export const parseOrderTree = (tree) => {
    tree.forEach((orderlist) => {
        setBigNumber(orderlist, 'price');
        setBigNumber(orderlist, 'quantity');
    });
};

/**
 * Parses settlement information pulled from API.
 */
export const parseSettlement = (settlement) => {
    setISODateTime(settlement, 'timestamp');
    settlement.isCustomerCounterPartyOne = settlement.counterPartyOneID === cache.getCustomerID();
    if (settlement.initiateChannel) {
        parseChannel(settlement.initiateChannel);
    }
    if (settlement.participateChannel) {
        parseChannel(settlement.participateChannel);
    }
};

/**
 * Returns a parsed settlement state in readiness for dispatch to API.
 */
export const parseSettlementState = (state) => {
    return parseEnum(SETTLEMENT_STATES, state, 'settlement state');
}

/**
 * Parses trade information pulled from API.
 */
export const parseTrade = (trade) => {
    setBigNumber(trade, 'price');
    setBigNumber(trade, 'quantity');
    setISODateTime(trade, 'timestamp');
};

/**
 * Returns a parsed trade state in readiness for dispatch to API.
 */
export const parseTradeState = (state) => {
    return parseEnum(TRADE_STATES, state, 'trade state');
}

/**
 * Parses transaction information pulled from API.
 */
export const parseTransaction = (tx) => {
    setISODateTime(tx, 'timestamp');
};

/**
 * Parses a UUID entered by user.
 */
export const parseUUID = (val) => {
    if (validateUUID(val) === false) {
        throw new Error('Value is not a valid UUID (v4)');
    }

    return val;
};

/**
 * Sets a big integernumerical value.
 */
const setISODateTime = (obj, slot) => {
    let timestamp = obj[slot] || new Date().getTime();
    timestamp = parseInt(timestamp);
    obj[slot] = DateTime.fromMillis(timestamp).toISO();
}

/**
 * Sets a big integernumerical value.
 */
const setBigNumber = (obj, slot, decimals=8) => {
    obj[slot] = obj[slot] ? new BigNumber(obj[slot]).toFixed(decimals) : null;
}
