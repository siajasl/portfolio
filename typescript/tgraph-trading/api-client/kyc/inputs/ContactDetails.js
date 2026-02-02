/**
 * @fileOverview Represents details regarding how to contact a person/entity.
 */

import { getEmail } from '../../utils/testing';
import { mapArray, mapObject, mapString } from '../../utils/gql/index';
import * as PostalAddress from './PostalAddress';
import * as TelephoneNumber from './TelephoneNumber';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        email:            ${mapString(obj.email)}
        postalAddress:    ${mapObject(obj.postalAddress, PostalAddress)}
        telephoneNumbers: ${mapArray(obj.telephoneNumbers, TelephoneNumber)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = () => {
    return {
        email:            getEmail(),
        postalAddress:    PostalAddress.create(),
        telephoneNumbers: ['Home', 'Mobile'].map((i) => TelephoneNumber.create(i))
    };
}
