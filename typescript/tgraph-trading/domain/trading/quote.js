/**
 * @fileOverview A quote for an order submitted to an exchange.
 */

const uuidv4 = require('uuid/v4');
const validateUUID = require('uuid-validate');
import { cloneDeep, isNil } from 'lodash';
import { BigNumber } from 'bignumber.js';
import { MatchingTypeEnum } from './enums';
import { AssetPair } from '../shared/index';
import { Book } from './book';
import { BookSideEnum } from './enums';
import { Order } from './order';
import { OrderTypeEnum } from './enums';
import { QuoteOptions } from './quoteOptions';

/**
 * A quote for an order placed by either a maker or taker.
 * @constructor
 * @param {object} details - Associated quote details.
 */
export class Quote {
    constructor ({
        addressOfBaseAsset,
        addressOfQuoteAsset,
        assetPair,
        customerOrderID,
        customerID,
        exchangeID,
        exchangeSymbol,
        options,
        price,
        quantity,
        quoteID,
        side,
        timestamp,
        type,
    }) {
        // Asset pair being traded, e.g. ETHBTC.
        this.assetPair = assetPair;

        // Customer's base asset DLT address, used to listen to events.
        this.addressOfBaseAsset = addressOfBaseAsset;

        // Customer's quote asset DLT address, used to listen to events.
        this.addressOfQuoteAsset = addressOfQuoteAsset;

        // Customer's identifier for internal cross-referencing purposes.
        this.customerOrderID = customerOrderID;

        // Customer's identifier, i.e. accessfile public key.
        this.customerID = customerID;

        // Exchange's unique identifier.
        this.exchangeID = exchangeID;

        // Exchange's unique symbol.
        this.exchangeSymbol = exchangeSymbol;

        // Various options affecting processing behaviour.
        this.options = options || new QuoteOptions({});

        // Price point at which quote is being placed.
        this.price = new BigNumber(price);

        // Quantity being placed.
        this.quantity = new BigNumber(quantity);

        // Internally unique quote identifier.
        this.quoteID = quoteID || uuidv4();

        // Side of order book to which quote applies, i.e. BUY | SELL.
        this.side = side;

        // Timestamp for various tracking scenarios.
        this.timestamp = timestamp || new Date();

        // Type of order being placed, i.e. LIMIT | MARKET.
        this.type = type;
    }

    /*
    * Returns a cloned instance - useful in test scenarios.
     */
    clone() {
        return cloneDeep(this);
    }

    /**
     * Instance validator - throws error if invalid.
     */
    validate() {
        if (this.assetPair instanceof AssetPair === false) {
            throw new Error('Invalid quote: asset pair is invalid')
        }

        if (isNil(this.addressOfBaseAsset)) {
            throw new Error('Invalid quote: base asset address is unspecified');
        }

        if (isNil(this.customerID)) {
            throw new Error('Invalid quote: customer ID is unspecified');
        }

        if (isNil(this.exchangeID)) {
            throw new Error('Invalid quote: exchange ID is unspecified');
        }

        if (this.price instanceof BigNumber === false) {
            throw new Error('Invalid quote: price');
        }

        if (this.quantity instanceof BigNumber === false) {
            throw new Error('Invalid quote: quantity');
        }

        if (isNil(this.addressOfQuoteAsset)) {
            throw new Error('Invalid quote: quote asset address is unspecified');
        }

        if (validateUUID(this.quoteID) === false) {
            throw new Error('Invalid quote: quote ID');
        }

        if (isNil(BookSideEnum[this.side])) {
            throw new Error(`Invalid quote: order book side`);
        }

        if (isNil(OrderTypeEnum[this.type])) {
            throw new Error('Invalid quote: type');
        }

        switch (this.type) {
            case OrderTypeEnum.LIMIT:
                if (this.price.isNaN() || this.price.lte(0)) {
                    throw new Error('Invalid quote: price is unspecified');
                }
                if (this.price.eq(this.price.dp(this.assetPair.quote.decimals, BigNumber.ROUND_DOWN)) === false) {
                    throw new Error('Invalid quote: price precision does not match quote asset precision');
                }
                break;

            case OrderTypeEnum.MARKET:
                if (this.price.isNaN() === false) {
                    throw new Error('Invalid quote: price market orders should not specify a price.');
                }
                break;
        }

        if (this.quantity.isNaN() || this.quantity.lte(0)) {
            throw new Error('Invalid quote: order quantity must be a valid number');
        }

        if (this.quantity.eq(this.quantity.dp(this.assetPair.base.decimals)) === false) {
            throw new Error('Invalid quote: order quantity precision does not match base asset precision');
        }
    }
}
