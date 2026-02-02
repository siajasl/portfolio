/**
 * @fileOverview Registers a level 1 individual customer with platform.
 */

import * as logger from '../../utils/logging';
import compose from '../shared/resolvers/factory';
import mapInputToDocument from '../../mappers/kyc/fromIndividualCustomerLevel1Input';

/**
 * Registers level 1 individual customer information.
 */
const resolve = async (parent, { input }, { dbe, smtp }, info) => {
    // Persist state.
    const document = mapInputToDocument(input);
    await dbe.addIndividualCustomerLevel1(document);

    // Dispatch notification.
    await smtp.dispatchNewIndividualCustomerLevel1(input);

    return true;
};

// Export composed resolver.
export default compose(
    resolve,
    'registering individual customer level 1',
);
