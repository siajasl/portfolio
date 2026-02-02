/**
 * @fileOverview Library wide exceptions.
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
        super('Feature is not implemented at this time', 'ERR_NOT_iMPLEMENTED');
    }
}

/**
 * Raised when secret seed is deemed to be invalid.
 * @constructor
 * @param {string} msg - Error message.
 */
export class InvalidSecretSeedError extends BaseError {
    constructor(msg) {
        super(msg, 'ERR_ENCODING_INVALID_PRIVATE_KEY');
    }
}

/**
 * Raised when a user password is deemed to be invalid.
 * @constructor
 */
export class InvalidPasswordError extends BaseError {
    constructor() {
        super(
            'User password must be >= 8 chars in length',
            'ERR_ENCODING_INVALID_PASSWORD',
        );
    }
}

/**
 * Raised when the decryption password is deemed to be incorrect.
 * @constructor
 */
export class IncorrectPasswordError extends BaseError {
    constructor() {
        super(
            'Password is incorrect',
            'ERR_DECODING_INCORRECT_PASSWORD'
        )
    }
}

/**
 * Raised when the access file version is unsupported.
 * @constructor
 */
export class UnsupportedVersionError extends BaseError {
    constructor() {
        super(
            'Unsupported version',
            'ERR_DECODING_UNSUPPORTED_VERSION'
        )
    }
}

/**
 * Raised when access file is asked to derive a key for an invalid coin.
 * @constructor
 * @param {string} symbol - Coin symbol.
 */
export class InvalidCoinSymbolError extends BaseError {
    constructor(symbol) {
        super(
            `${symbol} is an invalid chain symbol as per slip0044`,
            'ERR_INVALID_COIN_SYMBOL',
        );
    }
}

/**
 * Raised when access file is asked to derive a key for an invalid account index.
 * @constructor
 * @param {string} accountIndex - Account index.
 */
export class InvalidAccountError extends BaseError {
    constructor(accountIndex) {
        super(
            `${accountIndex} is an invalid account index as per bip44`,
            'ERR_INVALID_ACCOUNT_INDEX',
        );
    }
}

/**
 * Raised when access file is asked to derive a key for an invalid address index.
 * @constructor
 * @param {string} addressIndex - Address index.
 */
export class InvalidAddressError extends BaseError {
    constructor(addressIndex) {
        super(
            `${addressIndex} is an invalid address index as per bip44`,
            'ERR_INVALID_ADDRESS_INDEX',
        );
    }
}

/**
 * Raised when access file is asked to derive a key from weak entropy.
 * @constructor
 */
export class InvalidSeedError extends BaseError {
    constructor() {
        super(
            'Entropy source is invalid',
            'ERR_ENTROPY_INVALID',
        );
    }
}
