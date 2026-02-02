/**
 * @fileOverview Represents level 1 individual customer details.
 */

import { getHash } from '../../utils/testing';
import { mapString } from '../../utils/gql/index';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        customerID:            ${mapString(obj.customerID)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = (customerID) => {
    return {
        customerID:            customerID || getHash(),
    };
}
