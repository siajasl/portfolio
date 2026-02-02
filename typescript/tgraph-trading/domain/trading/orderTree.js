/**
 * @fileOverview A so-called red-black tree used to store OrderLists in price order.
 */

import { BigNumber } from 'bignumber.js';
import { SortedArray } from 'collections/sorted-array';
import { SortedMap } from 'collections/sorted-map';
import { Order } from './order';
import { OrderList } from './orderList';

/**
 * A red-black tree used to store OrderLists in price order.
 *
 * The exchange uses OrderTrees' to hold bid (black )and ask (red) data.
 * Storing data in a red-black tree makes it easier/faster to detect a match.
 */
export class OrderTree {
    constructor (side) {
        // Map: price (sorted) <-> order list.
        this.priceMap = new SortedMap();

        // Array: big numbers.
        this.prices = new SortedArray();

        // Map: order ID <-> order.
        this.orderMap = {};

        // Side of order book to which tree applies, i.e. BUY | SELL.
        this.side = side;
    }

    /**
     * Gets number of different prices in tree, i.e. depth.
     */
    get depth() {
        return this.prices.length;
    }

    /**
     * Returns flag indicating whether tree has orders at specified price point.
     */
    doesPriceExist(price) {
        return this.prices.has(price);
    }

    /**
     * Gets maximum price.
     */
    get maxPrice() {
        return this.depth > 0 ? this.prices.max() : null;
    }

    /**
     * Gets maximum price order list.
     */
    get maxPriceList() {
        return this.depth > 0 ? this.priceMap.get(this.maxPrice) : null;
    }

    /**
     * Gets minimum price.
     */
    get minPrice() {
        return this.depth > 0 ? this.prices.min() : null;
    }

    /**
     * Gets minimum price order list.
     */
    get minPriceList() {
        return this.depth > 0 ? this.priceMap.get(this.minPrice) : null;
    }

    /**
     * Gets total number of orders within tree.
     */
    get numberOfOrders() {
        return Object.keys(this.orderMap).length;
    }

    /**
     * Gets iterable of order lists being managed.
     */
    get orderLists() {
        return this.priceMap.values();
    }

    /**
     * Inserts an order into tree.
     */
    addOrder(order) {
        // Mutate prices.
        if (this.priceMap.has(order.price) === false) {
            const orderList = new OrderList(this.side, order.price);
            this.priceMap.add(orderList, order.price);
            this.prices.add(order.price);
        }

        // Mutate order list.
        this.priceMap.get(order.price).addOrder(order);

        // Mutate order map.
        this.orderMap[order.orderID] = order;
    }

    /**
     * Returns number of unfilled orders within tree.
     */
    getVolume() {
        let result = new BigNumber(0);
        for (const orderList of this.orderLists) {
            result = result.plus(orderList.getVolume());
        }

        return result;
    }

    /**
     * Returns flag indicating whether an order can in theory be matched.
     */
    canMatch(order, predicate) {
        for (const orderList of this.priceMap.values()) {
            if (orderList.canMatch(order, predicate)) {
                return true;
            }
        }
        return false;
    }

    /**
     * Removes an order from tree.
     */
    removeOrder (order) {
        // Mutate order list.
        const orderList = this.priceMap.get(order.price);
        orderList.removeOrder(order);

        // Mutate prices.
        if (orderList.length === 0) {
            this.priceMap.delete(order.price);
            this.prices.delete(order.price);
        }

        // Mutate order map.
        delete this.orderMap[order.orderID];
    }
}
