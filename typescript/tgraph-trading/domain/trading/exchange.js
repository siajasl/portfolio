/**
 * @fileOverview A virtual market for exchanging virtual assets.
 */

import { BigNumber } from 'bignumber.js';
import { isNil } from 'lodash';
import { AssetPair } from '../shared/index';
import { Book } from './book';
import { MatchingTypeEnum } from './enums';
import { Order } from './order';
import { OrderStateEnum } from './enums';
import { OrderTypeEnum } from './enums';
import { Quote } from './quote';
import { logInfo } from '../utils/logging';
import getMatcher from './matchers/factory';

/**
 * An entity facilitating the exchange of assets between buyers & sellers.
 * @constructor
 * @param {list} assets - Exchange's set of supported assets.
 * @param {list} assetPairs - Exchange's set of supported asset pairs.
 * @param {clearing.HtlcTimeouts} htlcTimeouts - Exchange's hashed time lock contract settings.
 * @param {string} commissionPercentage - Exchange's commission settings.
 * @param {string} id - Exchange's universally unique identifier.
 * @param {string} matchingAlgorithm - Exchange's matching algorithm.
 * @param {string} name - Exchange's trading name.
 * @param {string} symbol - Exchange's trading symbol.
 */
export class Exchange {
    constructor ({ assets, assetPairs, commissionPercentage, htlcTimeouts, id, matchingAlgorithm, name, symbol }) {
        // Exchange's set of assets (Map: string <-> Assets).
        this.assets = new Map(assets.map(i => [i.symbol, i]));

        // Exchange's set of asset pairs (Map: string <-> AssetPair).
        this.assetPairs = new Map(assetPairs.map(i => [i.symbol, i]));

        // Exchange's set of order books (Map: AssetPair <-> Book).
        this.books = new Map(assetPairs.map(i => [i.symbol, new Book(this, i)]));

        // Exchange's commission settings.
        this.commissionPercentage = new BigNumber(commissionPercentage);

        // Exchange's universally unique identifier.
        this.exchangeID = id;

        // Exchange's hashed timelock contract settings.
        this.htlcTimeouts = htlcTimeouts;

        // Exchange's trade matching handler.
        this.matcher = getMatcher(matchingAlgorithm);

        // Exchange's trade matching algorithm.
        this.matchingAlgorithm = matchingAlgorithm;

        // Exchange's trading name.
        this.name = name;

        // Exchange's trading symbol.
        this.symbol = symbol;
    }

    /*
     * Returns an asset pair definition.
     * @param {string} symbol - Symbol of asset pair being traded.
     */
    getAssetPair(symbol) {
        return this.assetPairs.get(symbol);
    }

    /*
     * Returns an order book.
     * @param {string} symbol - Symbol of asset pair being traded.
     */
    getBook(symbol) {
        return this.books.get(symbol);
    }

    /*
     * Removes an order from an exchange.
     * @param {String} orderID - Identifier of order to be retrieved from book.
     */
    getOrder(orderID) {
        for (const book of this.books.values()) {
            let order = book.getOrder(orderID);
            if (order) {
                return order;
            }
        }
    }

    /*
     * Removes an order from an exchange.
     * @param {Order} order - Order being removed from a book.
     */
    removeOrder(order) {
        // Defensive programming.
        this._validateOrder(order);

        // Get book & remove order.
        logInfo(`removing order from ${this.symbol}:${order.assetPair.symbol}: ${order.orderID}`);
        const book = this.getBook(order.assetPair.symbol);
        book.removeOrder(order);

        return { book, order };
    }

    /*
     * Re submits an order to the exchange - normally invoked after a redeployment.
     * @param {Order} order - An order being resubmitted to the exchange.
     */
    resubmitOrder(order) {
        // Defensive programming.
        this._validateOrder(order);

        // Resubmit.
        const book = this.getBook(order.assetPair.symbol);
        logInfo(`resubmitting order to ${this.symbol}:${book.symbol}: ${order.orderID}`);
        this.matcher.submit(book, order);
    }

    /*
     * Submits an order quote to an exchange.
     * @param {Quote} quote - A quote to be placed upon the exchange.
     */
    submitOrder(quote, emitLogMessage=true) {
        // Defensive programming.
        this._validateQuote(quote);

        // Instantiate.
        const order = new Order({ quote });

        // Submit.
        const book = this.getBook(order.assetPair.symbol);
        if (emitLogMessage) {
            logInfo(`submitting order to ${this.symbol}:${book.symbol}: ${order.orderID}`);
        }
        const result = this.matcher.submit(book, order);

        // If unmatched then update fill state.
        if (order.state === OrderStateEnum.NEW) {
            order.state = OrderStateEnum.UNFILLED;
        }

        return result;
    }

    /*
     * Validates an order quote prior to processing.
     * @param {Order} order - An order to be placed upon the exchange.
     */
    _validateOrder(order) {
        if (order instanceof Order === false) {
            throw new Error('Invalid order: must be an instance of Order')
        }
        this._validateQuote(order.quote);
        order.validate();
    }

    /*
     * Validates an order quote prior to processing.
     * @param {Quote} quote - A quote to be placed upon the exchange.
     */
    _validateQuote(quote) {
        if (quote instanceof Quote === false) {
            throw new Error('Invalid quote: must be an instance of Quote')
        }
        if (quote.assetPair instanceof AssetPair === false) {
            throw new Error('Invalid quote: invalid asset pair')
        }
        if (isNil(this.assetPairs.get(quote.assetPair.symbol))) {
            throw new Error('Invalid quote: unsupported asset pair')
        }
        if (this.matchingAlgorithm === MatchingTypeEnum.OTC && quote.type !== OrderTypeEnum.LIMIT) {
            throw new Error('Invalid quote: OTC exchanges process LIMIT orders only.');
        }
        quote.validate();
    }
}
