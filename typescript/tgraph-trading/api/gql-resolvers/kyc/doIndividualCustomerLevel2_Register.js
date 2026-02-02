/**
 * @fileOverview Registers a level 2 individual customer with platform.
 */

import compose from '../shared/resolvers/factory';
import mapInputToDocument from '../../mappers/kyc/fromIndividualCustomerLevel2Input';

/**
 * Registers level 2 individual customer information.
 */
const resolve = async (parent, { input }, { dbe, smtp }, info) => {
    // Persist state.
    const document = mapInputToDocument(input);
    await dbe.addIndividualCustomerLevel2(document);

    // Dispatch notification.
    await smtp.dispatchNewIndividualCustomerLevel2(input);

    return true;
};

// Export composed resolver.
export default compose(resolve, 'registering individual customer level 2');
