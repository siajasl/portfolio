/**
 * @fileOverview Represents level 1 individual customer details.
 */

import { getCountry, getHash } from '../../utils/testing';
import { mapArray, mapObject, mapString } from '../../utils/gql/index';
import * as BirthDetails from './BirthDetails';
import * as ContactDetails from './ContactDetails';
import * as Document from './Document';
import * as PersonName from './PersonName';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        birthDetails:             ${mapObject(obj.birthDetails, BirthDetails)}
        contactDetails:           ${mapObject(obj.contactDetails, ContactDetails)}
        customerID:               ${mapString(obj.customerID)}
        name:                     ${mapObject(obj.name, PersonName)}
        nationality:              ${mapString(obj.nationality)}
        verificationTxReference:  ${mapString(obj.verificationTxReference)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = (customerID) => {
    return {
        birthDetails:             BirthDetails.create(),
        contactDetails:           ContactDetails.create(),
        customerID:               customerID || getHash(),
        name:                     PersonName.create(),
        nationality:              getCountry(),
        verificationTxReference:  getHash(),
    };
}
