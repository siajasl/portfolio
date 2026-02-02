/**
 * @fileOverview An order book managing asset trading.
 */

import { BigNumber } from 'bignumber.js';
import { Order } from './order';
import { BookSideEnum } from './enums';
import { OrderStateEnum } from './enums';
import { OrderTree } from './orderTree';
import { Trade } from './trade';

/**
 * A book of orders.
 * @constructor
 * @param {Exchange} exchange - Associated exchange.
 * @param {AssetPair} assetPair - Asset pair being traded, e.g. ETHBTC.
 */
export class Book {
    constructor (exchange, assetPair) {
        // The books' sell (i.e. sell) side.
        this.asks = new OrderTree(BookSideEnum.SELL);

        // The books' underlying asset pair, e.g. ETHBTC.
        this.assetPair = assetPair;

        // The books' bid (i.e. buy) side.
        this.bids = new OrderTree(BookSideEnum.BUY);

        // Associated trading exchange.
        this.exchange = exchange;
    }

    /**
     * Gets total number of orders across book.
     */
    get numberOfOrders() {
        return this.asks.numberOfOrders + this.bids.numberOfOrders;
    }

    /*
     * Gets symbol of underlying asset pair being traded
     */
    get symbol() {
        return this.assetPair.symbol;
    }

    /*
     * Returns first order that matches passed identifier.
     */
    getOrder(orderID) {
        return this.asks.orderMap[orderID] || this.bids.orderMap[orderID];
    }

    /*
     * Returns order tree containing passed order.
     */
    getOrderTree(order) {
        return this.asks.orderMap[order.orderID] ? this.asks : this.bids;
    }

    /*
     * Removes order from book.
     */
    removeOrder(order) {
        this.getOrderTree(order).removeOrder(order);
    }

    /*
     * Submits an order for matching.
     * @param {Order} order - An order being submitted for processing.
     */
    submitOrder(order) {
        this.matcher.submit(this, order);
    }
}
