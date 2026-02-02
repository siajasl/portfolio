/**
 * @fileOverview Rejects level 1 individual customer registration.
 */

import compose from '../shared/resolvers/factory';
import mapInputToDocument from '../../mappers/kyc/fromRejectIndividualCustomerLevel1Input';

/**
 * Rejects level 1 individual customer registration.
 */
const resolve = async (parent, { input }, { dbe, smtp }, info) => {
    // Persist state.
    const document = mapInputToDocument(input);
    await dbe.updateIndividualCustomer(document);

    // Dispatch notification.
    await smtp.dispatchIndividualCustomerLevel1Rejected(input);

    return true;
};

// Export composed resolver.
export default compose(resolve, 'rejecting individual customer level 1');
