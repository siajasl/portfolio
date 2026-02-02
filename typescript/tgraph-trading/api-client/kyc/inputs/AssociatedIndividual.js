/**
 * @fileOverview Represents details regarding how to contact a person/entity.
 */

import { getBoolean, getCountry, getNumber, getProfession } from '../../utils/testing';
import { mapArray, mapObject, mapString } from '../../utils/gql/index';
import * as BirthDetails from './BirthDetails';
import * as ContactDetails from './ContactDetails';
import * as Document from './Document';
import * as IdentityCardScan from './IdentityCardScan';
import * as PersonName from './PersonName';

/**
 * Encodes instance as GQL input.
 */
 export const toGql = (obj) => {
     return `{
         birthDetails:           ${mapObject(obj.birthDetails, BirthDetails)}
         capacity:               ${mapString(obj.capacity)}
         contactDetails:         ${mapObject(obj.contactDetails, ContactDetails)}
         expectedYearlySalary:   ${obj.expectedYearlySalary}
         isPoliticallyExposed:   ${obj.isPoliticallyExposed}
         name:                   ${mapObject(obj.name, PersonName)}
         nationality:            ${mapString(obj.nationality)}
         proofOfEmployment:      ${mapObject(obj.proofOfEmployment, Document)}
         proofOfIdentity:        ${mapObject(obj.proofOfIdentity, IdentityCardScan)}
         proofOfResidency:       ${mapArray(obj.proofOfResidency, Document)}
         sourceOfFunds:          ${mapObject(obj.sourceOfFunds, Document)}
         sourceOfWealth:         ${mapObject(obj.sourceOfWealth, Document)}
     }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = () => {
    return {
        birthDetails:           BirthDetails.create(),
        capacity:               getProfession(),
        contactDetails:         ContactDetails.create(),
        expectedYearlySalary:   getNumber(),
        isPoliticallyExposed:   getBoolean(),
        name:                   PersonName.create(),
        nationality:            getCountry(),
        proofOfEmployment:      Document.create('Work contract'),
        proofOfIdentity:        IdentityCardScan.create(),
        proofOfResidency:       ['Utility Bill', 'Rental contract'].map((i) => Document.create(i)),
        sourceOfFunds:           Document.create('Bank statement'),
        sourceOfWealth:          Document.create('Accountant statement'),
    };
}
