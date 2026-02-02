/**
 * @fileOverview A standard matching algorithm: i.e. accepts limit/market orders.
 */

import { MatchingResult } from '../matchingResult';
import { Trade } from '../trade';

/*
 * Submits limit/market orders to a book.
 * @param {Book} book - An order book supported by an exchange.
 * @param {Order} order - An order being sumitted to an exchange.
 * @param {Function} [predicate] - A predicate to apply to determine whether an order can be matched or not.
 */
export const submit = (book, order, predicate) => {
    // Set descent predicate & order list getter;
    const handler = order.isLimit ? order.isAsk ? submitLimitAsk : submitLimitBid :
                                    order.isAsk ? submitMarketAsk : submitMarketBid;
    const [descentPredicate, orderListGetter] = handler(book, order, predicate);

    // Descend book - taking trades along the way.
    return processOrder(book, order, descentPredicate, orderListGetter);
}

/*
 * Submits a limit ask order to book.
 */
const submitLimitAsk = (book, order, predicate) => {
    return [
        // predicate determining when to stop book descent;
        () => order.price.lte(book.bids.maxPrice) &&
              book.bids.canMatch(order, predicate),

        // order list to be processed whilst descending book
        () => book.bids.maxPriceList
    ];
}

/*
 * Submits a limit bid order to book.
 */
const submitLimitBid = (book, order, predicate) => {
    return [
        // predicate determining when to stop book descent;
        () => order.price.gte(book.asks.minPrice) &&
              book.asks.canMatch(order, predicate),

        // order list to be processed whilst descending book
        () => book.asks.minPriceList
    ];
}

/*
 * Submits a market ask order to book.
 */
const submitMarketAsk = (book, order, predicate) => {
    return [
        // predicate determining when to stop book descent;
        () => book.bids.canMatch(order, predicate),

        // order list to be processed whilst descending book
        () => book.bids.maxPriceList
    ];
}

/*
 * Submits a market bid order to book.
 */
const submitMarketBid = (book, order, predicate) => {
    return [
        // predicate determining when to stop book descent;
        () => book.asks.canMatch(order, predicate),

        // order list to be processed whilst descending book
        () => book.asks.minPriceList
    ];
}

/*
 * Processes an order by filling trades as an order tree is descended.
 */
const processOrder = (book, order, descentPredicate, orderListGetter) => {
    let orderTree;
    let newTrades;
    let trades = [];

    // Match trades.
    orderTree = order.isBid ? book.asks : book.bids;
    while (order.isFilled === false && descentPredicate()) {
        newTrades = getTrades(book, orderTree, orderListGetter(), order);
        trades = trades.concat(newTrades);
    }

    // Add unfilled limit order to book.
    if (order.isLimit && order.isFilled === false) {
        orderTree = order.isBid ? book.bids : book.asks;
        orderTree.addOrder(order);
    }

    // Return matching result.
    return new MatchingResult(book, order, trades);
}

/*
 * Matches a tack order against make orders at a certain proce point.
 */
const getTrades = (book, orderTree, orderList, takeOrder) => {
    const trades = [];
    let fill;
    let makeOrder;

    // // Predicate determining when to stop order list processing.
    // const predicate = () => takeOrder.isFilled === false && orderList.length > 0;

    // Iterate whilst predicate is true.
    while (takeOrder.isFilled === false && orderList.length > 0) {
        // Take top order in list.
        makeOrder = orderList.head;

        // Set fill.
        fill = makeOrder.unfilled.gt(takeOrder.unfilled) ?
               takeOrder.unfilled : makeOrder.unfilled;

        // Set trade.
        trades.push(Trade.create(book, makeOrder, takeOrder, fill));

        // Remove filled make order from book.
        if (makeOrder.isFilled) {
            orderTree.removeOrder(makeOrder);
        }
    }

    return trades;
}

/*
 * Predicate returning flag indicating whether 2 orders can be matched.
 * @param {Order} o1 - An order being submitted to an exchange, i.e. a take.
 * @param {Order} o2 - An order submitted to an exchange, i.e. a make.
 */
export const canMatch = (o1, o2) => {
    // False if exchanges differ.
    if (o1.exchangeID !== o2.exchangeID) {
        return false;
    }

    // False if asset pairs sides differ.
    if (o1.assetPair !== o2.assetPair) {
        return false;
    }

    // False if book sides are identical.
    if (o1.side === o2.side) {
        return false;
    }

    // False if customer ID is identical.
    if (o1.customerID === o2.customerID) {
        return false;
    }

    // False if either order is filled.
    if (o1.isFilled || o2.isFilled) {
        return false;
    }

    return true;
};
