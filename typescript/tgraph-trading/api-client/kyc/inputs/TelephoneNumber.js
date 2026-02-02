/**
 * @fileOverview Represents a telephone number.
 */

import { getPhoneNumber } from '../../utils/testing';
import { mapString } from '../../utils/gql/index';
import TelephoneNumberType from './TelephoneNumberType';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        number:     ${mapString(obj.number)}
        numberType: ${obj.numberType}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = (numberType) => {
    return {
        number:     getPhoneNumber(),
        numberType:  numberType || TelephoneNumberType.Mobile
    };
}
