/**
 * @fileOverview A doubly linked list of Orders. Used to iterate through Orders when
    a price match is found. Each OrderList is associated with a single
    price. Since a single price match can have more quantity than a single
    Order, we may need multiple Orders to fullfill a transaction. The
    OrderList makes this easy to do. OrderList is naturally arranged by time.
    Orders at the front of the list have priority.
 */

import { BigNumber } from 'bignumber.js';

/**
 * A linked list of orders sorted by price.
 * @constructor
 * @param {BookSideEnum} side - Side of order book to which list applies.
 * @param {BigNumber} price - Price point at which orders are being placed.
 */
export class OrderList {
    constructor (side, price) {
        // First order in the list.
        this.head = null;

        // Last order in the list.
        this.tail = null;

        // Number of orders within linked list.
        this.length = 0;

        // Price point at which quote is being placed.
        this.price = price;

        // Side of the order book to which list applies.
        this.side = side;
    }

    /**
     * Append an order to list.
     */
    addOrder (order) {
        // If first order in list:
        if (this.length === 0) {
            order.next = null;
            order.previous = null;
            this.head = order;
            this.tail = order;

        // Append to last in list:
        } else {
            order.next = null;
            order.previous = this.tail;
            this.tail.next = order;
            this.tail = order;
        }

        this.length += 1;
    }

    /**
     * Returns flag indicating whether a matchable order exists within list.
     */
    canMatch(order, predicate) {
        if (this.length === 0) {
            return false;
        }

        let o = this.head;
        do {
            if (o.customerID !== order.customerID) {
                if (predicate === undefined) {
                    return true;
                } else if (predicate(order, o)) {
                    return true;
                }
            }
            o = o.next;
        } while (o !== null);

        return false;
    }

    /**
     * Returns number of unfilled orders within list.
     */
    getVolume() {
        if (this.head === null) {
            return new BigNumber(0);
        }

        let result = new BigNumber(0);
        let o = this.head;
        do {
            result = result.plus(o.unfilled);
            o = o.next;
        } while (o !== null);

        return result;
    }

    /**
     * Removes an order from list & relinks list.
     */
    removeOrder (order) {
        // Set next/previous of order being removed.
        const next = order.next
        const previous = order.previous

        // If next & previous orders:
        if (next && previous) {
            next.previous = previous
            previous.next = next

        // If next order only:
        } else if (next) {
            next.previous = null
            this.head = next

        // If previous order only:
        } else if (previous) {
            previous.next = null
            this.tail = previous

        // Otherwise reset:
        } else {
            this.head = null;
            this.tail = null;
        }

        this.length -= 1;
    }
}
