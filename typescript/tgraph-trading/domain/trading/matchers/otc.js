/**
 * @fileOverview A standard matching algorithm: i.e. accepts limit/market orders.
 */

import { MatchingResult } from '../matchingResult';
import { Trade } from '../trade';
import * as exceptions from '../exceptions';
import * as standard from './standard';

/*
 * Submits OTC orders to a book.
 * @param {Book} book - An order book supported by an OTC exchange.
 * @param {Order} order - An order being sumitted to an OTC exchange.
 */
export const submit = (book, order) => {
    const handler = order.options.otcOrderID ? take : make;
    return handler(book, order);
}

/*
 * Processes a make order.
 */
const make = (book, makeOrder) => {
    // Simply add to book.
    const orderTree = makeOrder.isAsk ? book.asks : book.bids;
    orderTree.addOrder(makeOrder);

    // Return matching result.
    return new MatchingResult(book, makeOrder, []);
}

/*
 * Processes a take order.
 */
const take = (book, takeOrder) => {
    // Set make order.
    const orderTree = takeOrder.isAsk ? book.bids : book.asks;
    const makeOrder = orderTree.orderMap[takeOrder.options.otcOrderID];

    // Make order must have identical price/quantity.

    // Make order must be matched by order ID.
    if (makeOrder === undefined) {
        throw new exceptions.InvalidOtcOrderIdError();
    }

    // Order customers must not match.
    if (makeOrder.customerID === takeOrder.customerID) {
        throw new exceptions.InvalidOtcCustomerIdError();
    }

    // Order prices must match.
    if (makeOrder.price.eq(takeOrder.price) === false) {
        throw new exceptions.OtcPriceMismatchError();
    }

    // Order quantities must match.
    if (makeOrder.quantity.eq(takeOrder.quantity) === false) {
        throw new exceptions.OtcQuantityMismatchError();
    }

    // Remove make order from tree.
    orderTree.removeOrder(makeOrder);

    // Set trade.
    const trade = Trade.create(book, makeOrder, takeOrder, takeOrder.quantity);

    // Return matching result.
    return new MatchingResult(book, takeOrder, [trade]);
}

/*
 * Predicate returning flag indicating whether 2 orders can be matched.
 * @param {Order} o1 - An order being submitted to an exchange, i.e. a take.
 * @param {Order} o2 - An order submitted to an exchange, i.e. a make.
 */
const canMatch = (o1, o2) => {
    // False if standard tests fail.
    if (standard.canMatch(o1, o2) === false) {
        return false;
    }

    // False if order prices are unmatched.
    if (o1.price.eq(o2.price)) {
        return false;
    }

    // False if order quantities are unmatched.
    if (o1.quantity.eq(o2.quantity)) {
        return false;
    }

    // False if both orders specify an otc order id.
    if (o1.options.otcOrderID && o2.options.otcOrderID) {
        return false;
    }

    // False if neither order specifies an otc order id.
    if (isNil(o1.options.otcOrderID) && isNil(o2.options.otcOrderID)) {
        return false;
    }

    // False if specified otc order id is unmatched.
    if ((o1.options.otcOrderID && o1.options.otcOrderID !== o2.orderID) ||
        (o2.options.otcOrderID && o2.options.otcOrderID !== o1.orderID)) {
        return false;
    }

    return true;
};
