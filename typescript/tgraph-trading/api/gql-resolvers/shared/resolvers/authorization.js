/**
 * @fileOverview Applies payload signature authentication.
 */

import { createError } from 'apollo-errors';

/**
 * Authenticates incoming request data against signature.
 */
export default (parent, { envelope }, { dbe }, info) => {
    // TODO if a closed endpoint then ensure customer is
    // registered in dB and has relevant access level.
};

/**
 * Thrown when request authorization fails.
 */
const AuthorizationError = createError('UnauthorizedError', {
    message: 'SECURITY :: USER IS UNAUTHORIZED'
});
