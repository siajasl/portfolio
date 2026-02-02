/**
 * @fileOverview Pulls corporate customer information from GQL server.
 */

import * as fragments from './utils/fragments';
import { execQuery, getEnvelope, mapString } from '../utils/gql/index';

// Action being executed.
const ACTION = 'getCorporateCustomer';

/**
 * Executes an API GQL query.
 */
export default async (data) => {
    const qry = getGql(data);

    return await execQuery(qry, ACTION);
};

/**
 * Returns GQL instruction to be process by server.
 */
const getGql = (data) => {
    return `
        query {
            ${ACTION}(
                input: {
                    customerID: ${mapString(data.customerID)}
                }
                envelope: ${getEnvelope(data, ACTION)}
            )
            {
                associatedParties {
                    directors {
                        ...associatedIndividualFields
                    },
                    shareholdingEntities {
                        ...associatedEntityFields
                    }
                    shareholdingIndividuals{
                        ...associatedIndividualFields
                    }
                    signatories{
                        ...associatedIndividualFields
                    }
                    uboEntities{
                        ...associatedEntityFields
                    }
                    uboIndividuals{
                        ...associatedIndividualFields
                    }
                }
                contactDetails {
                    ...contactDetailFields
                }
                customerID
                expectedYearlyInvestment
                isPoliticallyExposed
                name
                registrationNumber
                requiredDocuments {
                    articlesOfAssociation {
                        ...documentFields
                    }
                    certificateOfGoodStanding {
                        ...documentFields
                    }
                    certificateOfIncorporation {
                        ...documentFields
                    }
                    shareholdingStructure {
                        ...documentFields
                    }
                    sourceOfFunds {
                        ...documentFields
                    }
                    sourceOfWealth {
                        ...documentFields
                    }
                }
                verificationLevel
                verificationStatus
            }
        }

        # Fragments reused across queries.
        ${fragments.associatedEntityFields}
        ${fragments.associatedIndividualFields}
        ${fragments.birthDetailsFields}
        ${fragments.contactDetailFields}
        ${fragments.documentFields}
        ${fragments.personNameFields}
        ${fragments.proofOfIdentityFields}
    `;
};
