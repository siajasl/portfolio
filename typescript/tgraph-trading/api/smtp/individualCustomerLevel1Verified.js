/**
 * @fileOverview Dispatches a new individual customer level 1 email.
 */

// Module imports.
import * as logger from '../utils/logging';

export default async (input) => {
    const {
        email,
        customerID
    } = input;

    logger.logTODO('dispatch individual customer level 1 verified email');
}
