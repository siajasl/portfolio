/**
 * @fileOverview GQL fragments used across queries.
 */

export const associatedEntityFields = `
    fragment associatedEntityFields on AssociatedEntity {
        isPoliticallyExposed
        name
        registrationNumber
    }
`;

export const associatedIndividualFields = `
    fragment associatedIndividualFields on AssociatedIndividual {
        birthDetails {
            ...birthDetailsFields
        }
        capacity
        contactDetails {
            ...contactDetailFields
        }
        expectedYearlySalary
        isPoliticallyExposed
        name {
            ...personNameFields
        }
        nationality
        proofOfEmployment {
            ...documentFields
        }
        proofOfIdentity {
            ...proofOfIdentityFields
        }
        proofOfResidency {
            ...documentFields
        }
        sourceOfFunds {
            ...documentFields
        }
        sourceOfWealth {
            ...documentFields
        }
    }
`;

export const birthDetailsFields = `
    fragment birthDetailsFields on BirthDetails {
        country
        date
        place
    }
`;

export const contactDetailFields = `
    fragment contactDetailFields on ContactDetails {
        email
        postalAddress {
            ...postalAddressFields
        }
        telephoneNumbers {
            ...telephoneNumberFields
        }
    }

    fragment postalAddressFields on PostalAddress {
        streetLine01
        streetLine02
        streetLine03
        streetLine04
        city
        state
        county
        zipCode
        country
    }

    fragment telephoneNumberFields on TelephoneNumber {
        number
        numberType
    }
`;

export const documentFields = `
    fragment documentFields on Document {
        content
        documentType
        encoding
        jurisdiction
        validFromDate
        validToDate
    }
`;

export const personNameFields = `
    fragment personNameFields on PersonName {
        prefix
        first
        middle
        last
        suffix
    }
`;

export const proofOfIdentityFields = `
    fragment proofOfIdentityFields on IdentityCardScan {
        cardNumber
        cardType
        expiryDate
        scanEncoding
        scanOfFrontis
        scanOfReverse
        scanOfSelfie
    }
`;
