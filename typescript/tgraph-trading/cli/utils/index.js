/**
 * @fileOverview Sub-package initialser.
 */

import * as cache from './cache';
import * as io from './io/index';
import * as logging from './logging';
import * as session from './session';
import * as parsing from './parsing';
import * as config from './config';
import * as capitalizeFirstLetter from './capitalizeFirstLetter';
import executor from './executor';

/**
 * Mimics thread sleeping - used whilst awaiting event streams.
 */
const sleep = (milliseconds) => {
  return new Promise(resolve => setTimeout(resolve, milliseconds))
}

export {
    cache,
    executor,
    io,
    logging,
    parsing,
    session,
    config,
    capitalizeFirstLetter,
    sleep,
};
