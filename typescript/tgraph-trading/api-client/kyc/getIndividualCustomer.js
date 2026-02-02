/**
 * @fileOverview Pulls individual customer information from GQL server.
 */

import { execQuery, getEnvelope, mapString } from '../utils/gql/index';
import * as fragments from './utils/fragments';

// Action being executed.
const ACTION = 'getIndividualCustomer';

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
                birthDetails {
                    ...birthDetailsFields
                }
                contactDetails {
                    ...contactDetailFields
                }
                customerID
                expectedYearlyInvestment
                expectedYearlySalary
                isActingAsAgent
                isPoliticallyExposed
                name {
                    ...personNameFields
                }
                nationality
                principleEntity {
                  ...associatedEntityFields
                }
                principleIndividual {
                  ...associatedIndividualFields
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
