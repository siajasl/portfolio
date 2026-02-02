/**
 * @fileOverview Represents details regarding how to contact a person/entity.
 */

import { getBoolean, getCompanyName, getHash, getNumber } from '../../utils/testing';
import { mapObject, mapString } from '../../utils/gql/index';
import * as ContactDetails from './ContactDetails';
import * as CorporateCustomerAssociatedParties from './CorporateCustomer_AssociatedParties';
import * as CorporateCustomerRequiredDocuments from './CorporateCustomer_RequiredDocuments';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        associatedParties:        ${mapObject(obj.associatedParties, CorporateCustomerAssociatedParties)}
        contactDetails:           ${mapObject(obj.contactDetails, ContactDetails)}
        customerID:               ${mapString(obj.customerID)}
        expectedYearlyInvestment: ${obj.expectedYearlyInvestment}
        isPoliticallyExposed:     ${obj.isPoliticallyExposed}
        name:                     ${mapString(obj.name)}
        registrationNumber:       ${mapString(obj.registrationNumber)}
        requiredDocuments:        ${mapObject(obj.requiredDocuments, CorporateCustomerRequiredDocuments)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = (customerID) => {
    return {
        associatedParties:        CorporateCustomerAssociatedParties.create(),
        contactDetails:           ContactDetails.create(),
        expectedYearlyInvestment: getNumber(),
        customerID:               customerID || getHash(),
        isPoliticallyExposed:     getBoolean(),
        name:                     getCompanyName(),
        registrationNumber:       "520.1234.759",
        requiredDocuments:        CorporateCustomerRequiredDocuments.create(),
    };
}
