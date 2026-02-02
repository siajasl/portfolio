/**
 * @fileOverview Represents details regarding an entity associated with a corporation.
 */

import { getBoolean, getCompanyName } from '../../utils/testing';
import { mapObject, mapString } from '../../utils/gql/index';
import * as ContactDetails from './ContactDetails';
import * as Document from './Document';

/**
 * Returns GQL input mapped from an instance.
 */
export const toGql = (obj) => {
    return `{
        certificateOfIncorporation: ${mapObject(obj.certificateOfIncorporation, Document)}
        contactDetails:             ${mapObject(obj.contactDetails, ContactDetails)}
        isPoliticallyExposed:       ${obj.isPoliticallyExposed}
        name:                       ${mapString(obj.name)}
        registrationNumber:         ${mapString(obj.registrationNumber)}
        sourceOfFunds:              ${mapObject(obj.sourceOfFunds, Document)}
        sourceOfWealth:             ${mapObject(obj.sourceOfWealth, Document)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = () => {
    return {
        certificateOfIncorporation: Document.create('Certificate Of Incorporation'),
        contactDetails:             ContactDetails.create(),
        isPoliticallyExposed:       getBoolean(),
        name:                       getCompanyName(),
        registrationNumber:         "NM 52 09 54 C",
        sourceOfFunds:              Document.create('Bank statement'),
        sourceOfWealth:             Document.create('Accountant statement'),
    };
}
