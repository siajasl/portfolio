/**
 * @fileOverview A trade between 2 counter parties issued by a matching algorithm.
 */

const uuidv4 = require('uuid/v4');
import { TradeStateEnum } from './enums';

/**
 * A trade executed by 2 counter parties.
 * @constructor
 * @param {Order} makeOrder - An order matched from an order book.
 * @param {Order} takeOrder - An order being placed upon an exchange.
 * @param {BigNumber} quantity - Amount traded between counter-parties.
 */
export class Trade {
    constructor (book, makeOrder, takeOrder, quantity) {
        // Ammount being traded.
        this.amount = quantity;

        // Book upon which trade was matched.
        this.book = book;

        // Market maker's order.
        this.makeOrder = makeOrder;

        // Quantity of asset that was traded.
        this.quantity = quantity;

        // Processing state.
        this.state = TradeStateEnum.MATCHED;

        // Market taker's order.
        this.takeOrder = takeOrder;

        // Timestamp for various tracking scenarios.
        this.timestamp = new Date();

        // Internally unique identifier.
        this.tradeID = uuidv4();
    }

    /*
     * Asset pair being traded.
     */
    get assetPair() {
        return this.book.assetPair;
    }

    /*
     * Exchange to which orders were submitted.
     */
    get exchange() {
        return this.book.exchange;
    }

    /*
     * Exchange identifier to which orders were submitted.
     */
    get exchangeID() {
        return this.exchange.exchangeID;
    }

    /*
     * Exchange identifier to which orders were submitted.
     */
    get exchangeSymbol() {
        return this.exchange.symbol;
    }

    /*
     * Price agreed upon by counter parties.
     */
    get price() {
        return this.makeOrder.price;
    }

    /**
     * Static factory method.
     * @param {Book} book - An order book.
     * @param {Order} makeOrder - An order matched from an order book.
     * @param {Order} takeOrder - An order being placed upon an exchange.
     * @param {BigNumber} quantity - Amount traded between counter-parties.
     */
    static create (book, makeOrder, takeOrder, quantity) {
        // Update orders.
        makeOrder.setFill(quantity);
        takeOrder.setFill(quantity);

        // Return new instance.
        return new Trade(book, makeOrder, takeOrder, quantity);
    }
}
