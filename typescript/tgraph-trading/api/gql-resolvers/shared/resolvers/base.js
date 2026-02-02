/**
 * @fileOverview Base resolver from which all other resolvers are composed.
 */

import { createError, isInstance } from 'apollo-errors';
import * as logger from '../../../utils/logging';


/**
 * Handles unmanaged errors thrown by child resolvers.
 */
export const resolveError = (root, { envelope }, context, error) => {
    logger.logError(error);
    return isInstance(error) ? error : new UnmanagedError();
};

/**
 * Thrown when request processing fails for unknown reasons.
 */
const UnmanagedError = createError('UnmanagedError', {
    message: 'UNMANAGED ERROR OCCURRED'
});
