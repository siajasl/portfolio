/**
 * @fileOverview Returns individual customer information.
 */

import compose from '../shared/resolvers/factory';
import mapDocumentToOutput from '../../mappers/kyc/toIndividualCustomer';

/**
 * Returns individual customer information from dB.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    // Pull document from dB.
    const document = await dbe.getIndividualCustomer(input.customerID);

    // Return mapped GQL output.
    return document ? mapDocumentToOutput(document) : null;
};

// Export composed resolver.
export default compose(resolve, 'retrieving individual customer data');
