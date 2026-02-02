/**
 * @fileOverview Exceptions thrown by trading engine.
 */

/**
 * Managed exceptions base class.
 * @constructor
 * @param {string} err - Error message.
 * @param {string} code - Error code.
 */
export class BaseError extends Error {
    constructor(message, code) {
        super(message);
        this.code = code;
        this.name = this.constructor.name;
    }
}

/**
 * Error thrown when a feature is invoked but not yet implemented.
 */
export const ERR_ORDER_BOOK_ASSET_PAIR_INVALID = 'ERR_ORDER_BOOK_ASSET_PAIR_INVALID';
export class AssetPairInvalid extends BaseError {
    constructor(assetPair) {
        super(`Asset pair (${assetPair}) is invalid`, ERR_ORDER_BOOK_ASSET_PAIR_INVALID);
    }
}

/**
 * Error thrown when a feature is invoked but not yet implemented.
 */
export const ERR_ORDER_QUANTITY_INVALID = 'ERR_ORDER_QUANTITY_INVALID';
export class InvalidOrderQuantityError extends BaseError {
    constructor() {
        super(`Order quantity must be greater than zero`, ERR_ORDER_QUANTITY_INVALID);
    }
}

/**
 * Error thrown when a feature is invoked but not yet implemented.
 */
export const ERR_ORDER_PRICE_INVALID = 'ERR_ORDER_PRICE_INVALID';
export class InvalidOrderPriceError extends BaseError {
    constructor(msg) {
        super(msg, ERR_ORDER_PRICE_INVALID);
    }
}

/**
 * Error thrown when an OTC take order cannot be matched to an OTC make order.
 */
export const ERR_OTC_INVALID_CUSTOMER_ID = 'ERR_OTC_INVALID_CUSTOMER_ID';
export class InvalidOtcCustomerIdError extends BaseError {
    constructor() {
        super(`Matched OTC orders must be by different customers`, ERR_OTC_INVALID_CUSTOMER_ID);
    }
}

/**
 * Error thrown when an OTC take order cannot be matched to an OTC make order.
 */
export const ERR_OTC_INVALID_ORDER_ID = 'ERR_OTC_INVALID_ORDER_ID';
export class InvalidOtcOrderIdError extends BaseError {
    constructor() {
        super(`Active OTC order not found`, ERR_OTC_INVALID_ORDER_ID);
    }
}

/**
 * Error thrown when an OTC order has a different price to it's matched order.
 */
export const ERR_OTC_PRICE_MISMATCH = 'ERR_OTC_PRICE_MISMATCH';
export class OtcPriceMismatchError extends BaseError {
    constructor() {
        super(`Matched OTC orders must be at same price`, ERR_OTC_PRICE_MISMATCH);
    }
}

/**
 * Error thrown when an OTC order has a different quantity to it's matched order.
 */
export const ERR_OTC_QUANTITY_MISMATCH = 'ERR_OTC_QUANTITY_MISMATCH';
export class OtcQuantityMismatchError extends BaseError {
    constructor() {
        super(`Matched OTC orders must be for same quantity`, ERR_OTC_QUANTITY_MISMATCH);
    }
}
