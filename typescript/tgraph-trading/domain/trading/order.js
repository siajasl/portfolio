/**
 * @fileOverview An order being processed by the trading engine.
 */

import { isNil } from 'lodash';
import { BigNumber } from 'bignumber.js';
const uuidv4 = require('uuid/v4');
const validateUUID = require('uuid-validate');
import { BookSideEnum } from './enums';
import { OrderStateEnum } from './enums';
import { OrderTypeEnum } from './enums';

/**
 * Either a bid or ask order with a book.  Orders are double-linked to help
 * exchange fullfill orders with quantities larger than a single order.
 * @constructor
 * @param {Quote} quote - An order quote being placed upon an exchange.
 */
export class Order {
    constructor ({
        filled,
        orderID,
        quote,
        state,
        timestamp
    }) {
        // Quantity filled by trading engine.
        this.filled = filled ? new BigNumber(filled) : new BigNumber(0);

        // Next order in associated order list - simplifies order processing at a particular price point.
        this.next = null;

        // Internally unique order identifier.
        this.orderID = orderID || uuidv4();

        // Previous order in associated order list - simplifies order processing at a particular price point.
        this.previous = null;

        // Quantity being placed upon book - derived from quotes unfilled qauntity.
        this.quantity = new BigNumber(quote.quantity);

        // Associated quote placed by customer.
        this.quote = quote;

        // Order processing state.
        this.state = state || OrderStateEnum.NEW;

        // Timestamp for various tracking scenarios.
        this.timestamp = timestamp || new Date();
    }

    /**
     * Sets a fill subsequent to an order matching event.
     */
    setFill(quantity) {
        // Increment filled amount.
        this.filled = this.filled.plus(quantity);

        // Update fill state.
        if (this.isFilled) {
            this.state = OrderStateEnum.FILLED;
        }
        else if (this.isPartiallyFilled) {
            this.state = OrderStateEnum.PARTIALLY_FILLED;
        }
        else {
            this.state = OrderStateEnum.UNFILLED;
        }
    }

    /**
     * Instance validator - throws error if invalid.
     */
    validate() {
        if (this.filled instanceof BigNumber === false) {
            throw new Error('Invalid order: fill quantity');
        }

        if (validateUUID(this.orderID) === false) {
            throw new Error('Invalid order: order ID');
        }

        if (this.quantity instanceof BigNumber === false) {
            throw new Error('Invalid order: quantity');
        }

        if (isNil(OrderStateEnum[this.state])) {
            throw new Error('Invalid order: state');
        }

        if (isNil(this.quote)) {
            throw new Error('Invalid order: quote');
        }
        this.quote.validate();
    }

    /*
     * Asset pair being traded, e.g. ETHBTC.
     */
    get assetPair() {
        return this.quote.assetPair;
    }

    /*
     * Returns quantity in terms of base asset.
     */
    get baseAmount() {
        return this.assetPair.getBaseAmount(this.price, this.quantity);
    }

    /*
     * Customer's identifier for internal cross-referencing purposes.
     */
    get customerID() {
        return this.quote.customerID;
    }

    /*
     * Exchange identifier to which order was submitted.
     */
    get exchangeID() {
        return this.quote.exchangeID;
    }

    /*
     * Returns a flag indicating whether quote is an ask, i.e. an offer to sell.
     */
    get isAsk() {
        return this.quote.side === BookSideEnum.SELL;
    }

    /*
     * Returns a flag indicating whether quote is a bid, i.e. an offer to buy.
     */
    get isBid() {
        return this.quote.side === BookSideEnum.BUY;
    }

    /*
     * Returns flag indicating whether the order is filled or not.
     */
    get isFilled() {
        return this.unfilled.lte(0);
    }

    /*
     * Returns flag indicating whether order type = LIMIT.
     */
    get isLimit() {
        return this.type === OrderTypeEnum.LIMIT;
    }

    /*
    * Returns flag indicating whether order type = MARKET.
     */
    get isMarket() {
        return this.type === OrderTypeEnum.MARKET;
    }

    /*
     * Returns flag indicating whether the order is partially filled or not.
     */
    get isPartiallyFilled() {
        return this.isFilled === false && this.unfilled.lt(this.quantity);
    }

    /*
     * Various options affecting processing behaviour.
     */
    get options() {
        return this.quote.options;
    }

    /*
     * Price point at which quote is being placed.
     */
    get price() {
        return this.quote.price;
    }

    /*
     * Returns quantity in terms of base asset.
     */
    get quoteAmount() {
        return this.assetPair.getQuoteQuantity(this.price, this.quantity);
    }

    /*
     * Side of order book to which quote applies, i.e. BUY | SELL.
     */
    get side() {
        return this.quote.side;
    }

    /*
     * Type of order being placed.
     */
    get type() {
        return this.quote.type;
    }

    /*
     * Returns quantity remaining to be traded upon book.
     */
    get unfilled() {
        return this.quantity.minus(this.filled);
    }
}
