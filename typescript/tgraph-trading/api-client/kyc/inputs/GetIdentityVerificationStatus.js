/**
 * @fileOverview Represents information required to retrieve a customer's verification status.
 */

import { getHash } from '../../utils/testing';
import { mapString } from '../../utils/gql/index';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        customerID:               ${mapString(obj.customerID)}
        verificationTxReference:  ${mapString(obj.verificationTxReference)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = (customerID) => {
    return {
        customerID:               customerID || getHash(),
        verificationTxReference:  getHash()
    };
}
