/**
 * @fileOverview A trade between 2 counter parties issued by a matching algorithm.
 */

const uuidv4 = require('uuid/v4');
import { TradeStateEnum } from './enums';

/**
 * Encapsulate trade engine matching result information.
 * @constructor
 * @param {Book} book - Book upon which trades were matched.
 * @param {Order} order - Order that was passed through a matcher.
 * @param {[Trade]} trades - Set of matched trades.
 */
export class MatchingResult {
    constructor (book, order, trades) {
        // Pair of assets being traded.
        this.assetPair = book.assetPair;

        // Book upon which trades were matched.
        this.book = book;

        // Take order that was matched.
        this.order = order;

        // Take order quote information.
        this.quote = order.quote;

        // Set of matched trades.
        this.trades = trades;
    }
}
