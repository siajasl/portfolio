/**
 * @fileOverview Exceptions thrown by the library.
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
export class NotImplementedError extends BaseError {
  constructor() {
    super('Feature is not implemented at this time', 'ERR_NOT_IMPLEMENTED');
  }
}

/**
 * Raised when environment settings are invalid.
 */
export class InvalidEnvironmentError extends BaseError {
  constructor() {
    super(
      'Environment is unsupported - review your settings',
      'ERR_ENVIRONMENT_INVALID',
    );
  }
}

/**
 * Raised when a channel is not found within dB when resolving by address.
 */
export class ChannelNotFoundByAddressError extends BaseError {
  constructor(address) {
    super(`Channel not found: address=${address}`, 'ERR_CHANNEL_NOT_FOUND');
  }
}

/**
 * Raised when an exchange address is not found within dB.
 */
export class ExchangeAddressError extends BaseError {
  constructor(address) {
    super(
      `Exchange address unrecognised: address=${address}`,
      'ERR_EXCHANGE_ADDRESS_NOT_FOUND',
    );
  }
}

/**
 * Raised when a channel is not found within dB.
 */
export class ChannelNotFoundError extends BaseError {
  constructor(tradeId, deposit) {
    super(
      `Channel not found: tradeId=${tradeId} :: deposit=${deposit}`,
      'ERR_CHANNEL_NOT_FOUND',
    );
  }
}

/**
 * Raised when a customer's deposit balance is insufficient to perform a trade.
 */
export class DepositBalanceInsufficientError extends BaseError {
  constructor() {
    super(
      'Insufficient deposit balance to fulfil trade amount',
      'ERR_DEPOSIT_BALANCE_INSUFFICIENT_ERROR',
    );
  }
}

/**
 * Raised when a deposit is not found within dB.
 */
export class DepositNotFoundError extends BaseError {
  constructor(identifier) {
    super(`Deposit not found: id=${identifier}`, 'ERR_DEPOSIT_NOT_FOUND');
  }
}

/**
 * Raised when the dispatch of an email fails.
 */
export class EmailDispatchError extends BaseError {
  constructor(message) {
    super(
      `MailGun email dispatch failed - review your settings:: ${message}`,
      'ERR_EMAIL_DISPATCH_FAILURE',
    );
  }
}

/**
 * Raised when the dispatch of an web push fails.
 */
export class WebPushDispatchError extends BaseError {
  constructor(message) {
    super(
      `OneSignal web push dispatch failed - review your settings:: ${message}`,
      'ERR_WEB_PUSH_DISPATCH_FAILURE',
    );
  }
}

/**
 * Raised when an HTLC address is unresolvable.
 */
export class HTLCAddressError extends BaseError {
  constructor(symbol, address) {
    super(`${symbol} HTLC not found @ ${address}`, 'ERR_HTLC_NOT_FOUND');
  }
}

/**
 * Raised when an HTLC transfer is missing a signature.
 */
export class HTLCTransferSignatureError extends BaseError {
  constructor(symbol) {
    super(
      `Missing recipient signature in ${symbol}.transferOutOfHtlc`,
      'ERR_HTLC_NOT_FOUND',
    );
  }
}

/**
 * Raised when the retrieval of an asset's FIAT price fails.
 */
export class FIATPriceRetrievalError extends BaseError {
  constructor(ticker, message) {
    super(
      `${ticker} FIAT (USD) price retrieval fails: ${message}`,
      'ERR_FIAT_PRICE_RETRIEVAL_FAILURE',
    );
  }
}

/**
 * Raised when a limit order's price is invalid.
 */
export class LimitOrderPriceInvalidError extends BaseError {
  constructor({ price }) {
    super(
      `Order price must be a positive integer: ${price}`,
      'ERR_LIMIT_ORDER_PRICE_INVALID',
    );
  }
}

/**
 * Raised when a limit order's price is null.
 */
export class LimitOrderPriceNullError extends BaseError {
  constructor() {
    super(`Limit order price must be set`, 'ERR_LIMIT_ORDER_PRICE_NULL');
  }
}

/**
 * Raised when a market order's price is set (it should be null).
 */
export class MarketOrderPriceNotNullError extends BaseError {
  constructor({ price }) {
    super(
      `Market order should unset price: ${price}`,
      'ERR_MARKET_ORDER_PRICE_INVALID',
    );
  }
}

/**
 * Raised when an order book is unsupported.
 */
export class BookNotFoundError extends BaseError {
  constructor(symbol) {
    super(`Order book not found: symbol=${symbol}`, 'ERR_ORDER_BOOK_NOT_FOUND');
  }
}

/**
 * Raised when an order is not found within dB.
 */
export class OrderNotFoundError extends BaseError {
  constructor(identifier) {
    super(`Order not found: id=${identifier}`, 'ERR_ORDER_NOT_FOUND');
  }
}

/**
 * Raised when an order's quantity is invalid.
 */
export class OrderQuantityError extends BaseError {
  constructor(quantity) {
    super(
      `Order quantity must be a positive integer: ${quantity}`,
      'ERR_ORDER_QUANTITY_INVALID',
    );
  }
}

/**
 * Raised when an order's quantity is invalid due to a decimal check error.
 */
export class OrderQuantityDecimalError extends BaseError {
  constructor(value, decimals) {
    super(
      `${value} contains more than ${decimals} decimal places`,
      'ERR_ORDER_QUANTITY_DECIMAL_ERROR',
    );
  }
}

/**
 * Raised when the order side is invalid.
 */
export class OrderSideError extends BaseError {
  constructor() {
    super(
      `Order side is invalid - valid sides= BUY, SELL`,
      'ERR_ORDER_SIDE_INVALID',
    );
  }
}

/**
 * Raised when an order's quantity is below accepted threshold.
 */
export class OrderQuantityThresholdError extends BaseError {
  constructor(costUSD, thresholdUSD) {
    super(
      `Order quantity below threshold: cost=$${costUSD}, minimum=$${thresholdUSD}`,
      'ERR_ORDER_QUANTITY_THRESHOLD_ERROR',
    );
  }
}

/**
 * Raised when an order of unknown type is submitted.
 */
export class OrderTypeError extends BaseError {
  constructor() {
    super(
      `Order type is invalid - valid types = LIMIT, MARKET`,
      'ERR_ORDER_TYPE_INVALID',
    );
  }
}

/**
 * Raised when a trade is not found within dB.
 */
export class TradeNotFoundError extends BaseError {
  constructor(identifier) {
    super(`Trade not found: id=${identifier}`, 'ERR_TRADE_NOT_FOUND');
  }
}

/**
 * Raised when a trade's double encrypted secret is unassigned.
 */
export class TradeEncryptedSecretUnassigned extends BaseError {
  constructor(identifier) {
    super(
      `Trade encrypted secret non-existent: id=${identifier}`,
      'ERR_TRADE_ENCRYPTED_SECRET_UNASSIGNED_ERROR',
    );
  }
}

/**
 * Raised when a customer's beneficiary address lookup fails.
 */
export class CustomerBeneficiaryAddressLookupError extends BaseError {
  constructor() {
    super(
      `Unable to determine beneficiary address`,
      'ERR_USER_BENEFICIARY_ADDRESS_LOOKUP_ERROR',
    );
  }
}

/**
 * Raised when a customer's deposit lookup fails.
 */
export class CustomerDepositLookupError extends BaseError {
  constructor(address) {
    super(`Deposit not found: ${address}`, 'ERR_USER_DEPOSIT_LOOKUP_ERROR');
  }
}

/**
 * Raised when a customer is not found within dB.
 */
export class CustomerNotFoundError extends BaseError {
  constructor(identifier) {
    super(`Customer not found: id=${identifier}`, 'ERR_USER_NOT_FOUND');
  }
}

/**
 * Raised when a (public) key verification fails.
 */
export class PublicKeyVerificationError extends BaseError {
  constructor(pbk) {
    super(
      `Public key verification failed: ${pbk}`,
      'ERR_USER_PUBLIC_KEY_VERIFICATION_ERROR',
    );
  }
}

/**
 * Raised when a (public) key verification fails.
 */
export class CustomerIDVerificationError extends BaseError {
  constructor(customerID) {
    super(
      `Customer ID verification failed: ${customerID}`,
      'ERR_USER_ID_VERIFICATION_ERROR',
    );
  }
}
