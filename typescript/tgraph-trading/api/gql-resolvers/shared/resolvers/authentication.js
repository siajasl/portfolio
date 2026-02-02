/**
 * @fileOverview Applies payload signature authentication.
 */

import * as _ from 'lodash';
import { createError } from 'apollo-errors';
import { verifyPayloadSignature, verifyCustomerID } from '../../../utils/security';

/**
 * Authenticates incoming request data against signature.
 */
export default (parent, { envelope, input }, context, info) => {
    authenticateRequest(envelope, input);
};

/**
 * Authenticates incoming request input against request envelope signature.
 */
export const authenticateRequest = (payload, envelope) => {
    // Verify payload signature.
    if (_.isNil(envelope.signature) === false) {
        try {
            verifyPayloadSignature(payload, envelope);
        } catch (err) {
            throw new AuthenticationError();
        }
    }

    // Verify customer ID matches wallet ID.
    if (_.isNil(payload.customerID) === false) {
        try {
            verifyCustomerID(payload, envelope);
        } catch (err) {
            throw new AuthenticationError();
        }
    }
}

/**
 * Thrown when request data signature validation fails.
 */
const AuthenticationError = createError('InvalidRequestSignatureError', {
    message: 'SECURITY :: INVALID REQUEST SIGNATURE'
});
