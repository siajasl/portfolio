/**
 * @fileOverview Represents a person's identity card.
 */

import { chance } from '../../utils/testing';
import { mapDate, mapObject, mapString } from '../../utils/gql/index';
import * as Document from './Document';
import IdentityCardType from './IdentityCardType';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        cardNumber:     ${mapString(obj.cardNumber)}
        cardType:       ${obj.cardType}
        expiryDate:     ${mapDate(obj.expiryDate)}
        scanEncoding:   ${mapString(obj.scanEncoding)}
        scanOfFrontis:  ${mapString(obj.scanOfFrontis)}
        scanOfReverse:  ${mapString(obj.scanOfReverse)}
        scanOfSelfie:   ${mapString(obj.scanOfSelfie)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = (cardType) => {
    return {
        cardNumber:         "500294567",
        cardType:           cardType || IdentityCardType.Passport,
        expiryDate:         Date.now(),
        scanEncoding:       "image/jpeg",
        scanOfFrontis:      "Passport scan - reverse",
        scanOfReverse:      "Passport scan - reverse",
        scanOfSelfie:       "Selfie scan - reverse",
    };
}
