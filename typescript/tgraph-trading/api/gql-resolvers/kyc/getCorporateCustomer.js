/**
 * @fileOverview Returns corporate customer information.
 */

import compose from '../shared/resolvers/factory';
import mapDocumentToOutput from '../../mappers/kyc/toCorporateCustomer';

/**
 * Returns corporate customer information from dB.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    // Pull document from dB.
    const document = await dbe.getCorporateCustomer(input.customerID);

    // Return mapped GQL output.
    return document ? mapDocumentToOutput(document) : null;
};

// Export composed resolver.
export default compose(resolve, 'retrieving corporate customer data');
