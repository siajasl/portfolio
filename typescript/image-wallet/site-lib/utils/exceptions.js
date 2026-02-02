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
 * Wraps decoding pipeline errors.
 */
export class DecodingError extends BaseError {
    constructor(err, code) {
        super('Decoder :: ' + err, code || 'ERR_DECODING');
    }
}

/**
 * Raised when access file png file is deemed to be invalid.
 * @constructor
 * @param {string} msg - Error message.
 */
export class InvalidPngFileError extends DecodingError {
    constructor(msg) {
        super(msg, 'ERR_DECODING_INVALID_PMG_FILE');
    }
}

/**
 * Raised when the access file version is unsupported.
 * @constructor
 */
export class UnsupportedVersionError extends DecodingError {
    constructor() {
        super(
            'Unsupported version',
            'ERR_DECODING_UNSUPPORTED_VERSION'
        )
    }
}

/**
 * Raised when the number of KDF rounds is too large.
 * @constructor
 */
export class ExcessiveRoundsError extends DecodingError {
    constructor() {
        super(
            'Excessive number of KDF rounds',
            'ERR_DECODING_EXCESSIVE_ROUNDS'
        )
    }
}
