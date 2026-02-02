/**
 * @fileOverview Registers a corporate customer with platform.
 */

import compose from '../shared/resolvers/factory';
import mapInputToDocument from '../../mappers/kyc/fromCorporateCustomerInput';

/**
 * Registers corporate customer information.
 */
const resolve = async (parent, { input }, { dbe, smtp }, info) => {
    // Persist state.
    const document = mapInputToDocument(input);
    await dbe.addCorporateCustomer(document);

    // Dispatch notification.
    await smtp.dispatchNewCorporateCustomer(input);

    return true;
};

// Export composed resolver.
export default compose(resolve, 'registering corporate customer');
