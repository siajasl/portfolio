/**
 * @fileOverview Library logging utility functions.
 */

// Set of logging levels.
const LOG_LEVEL_DEBUG = 'DEBUG';
const LOG_LEVEL_ERROR = 'ERROR';
const LOG_LEVEL_INFO = 'INFO';
const LOG_LEVEL_TODO = 'TODO';
const LOG_LEVEL_WARNING = 'WARN';

/**
 * Emits a logging message.
 *
 * @param {string} msg - Message to be logged.
 * @param {string} level - Level of importance.
 */
export const log = (msg, level) => {
    // TODO: move to 3rd party logging utility.
    console.log(get_formatted_message(msg, level));
};

/**
 * Emits a logging message for debug purposes.
 *
 * @param {string} msg - Message to be logged.
 */
export const logDebug = (msg) => {
    log(msg, LOG_LEVEL_DEBUG);
};

/**
 * Emits a logging message for error purposes.
 *
 * @param {Error} err - Trapped error.
 */
export const logError = (err) => {
    const msg = err.code + ' :: ' + err.message;
    log(msg, LOG_LEVEL_ERROR);
};

/**
 * Emits a logging message for information purposes.
 *
 * @param {string} msg - Message to be logged.
 */
export const logInfo = (msg) => {
    log(msg, LOG_LEVEL_INFO);
};

/**
 * Emits a logging message for warning purposes.
 *
 * @param {string} msg - Message to be logged.
 */
export const logWarning = (msg) => {
    log(msg, LOG_LEVEL_WARNING);
};

/**
 * Emits a logging message for TODO purposes.
 *
 * @param {string} msg - Message to be logged.
 */
export const logTODO = (msg) => {
    log('todo: ' + msg, LOG_LEVEL_TODO);
};

/**
 * Formats a message to be logged.
 *
 * @param {msg} Message to be logged.
 * @param {level} Level of importance.
 */
const get_formatted_message = (msg, level) => {
  return [
        new Date().toISOString(),
        'DOM',
        `[${level || LOG_LEVEL_INFO}]`,
        msg,
    ].join(' :: ');
};
