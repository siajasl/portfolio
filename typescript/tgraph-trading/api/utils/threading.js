/**
 * @fileOverview Thread related utility functions.
 */

/**
 * Mimics thread sleeping.
 * @param {Number} milliseconds - Time duration in milliseconds for which to sleep.
 */
export const sleep = (milliseconds) => {
  return new Promise(resolve => setTimeout(resolve, milliseconds))
}
