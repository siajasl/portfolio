/**
 * @fileOverview Level 1 individual customer verification details.
 */

import { getHash } from '../../utils/testing';
import { mapObject, mapString } from '../../utils/gql/index';

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
export const create = (customerID, verificationTxReference) => {
    return {
        customerID:               customerID || getHash(),
        verificationTxReference:  verificationTxReference || getHash(),
    };
}
