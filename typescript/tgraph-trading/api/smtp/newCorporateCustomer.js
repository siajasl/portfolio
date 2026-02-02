/**
 * @fileOverview Dispatches a new corporate customer email.
 */

// Module imports.
import * as logger from '../utils/logging';

export default async (input) => {
    const {
        email,
        customerID
    } = input;

    logger.logTODO('dispatch new corporate customer email');
}
