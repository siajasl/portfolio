/**
 * @fileOverview Verifies level 1 individual customer registration.
 */

import compose from '../shared/resolvers/factory';
import mapInputToDocument from '../../mappers/kyc/fromVerifyIndividualCustomerLevel1Input';
import * as idVerifier from './idVerifier/index'

/**
 * Verifies level 1 individual customer registration.
 */
const resolve = async (parent, { input }, { dbe, smtp }, info) => {
    // Pull proof of identity information from identity verifer.
    const { verificationTxReference } = input;
    const proofOfIdentity = await idVerifier.getProofOfIdentity(verificationTxReference);

    // Persist state.
    const document = mapInputToDocument({
        ...input,
        proofOfIdentity
    });
    await dbe.updateIndividualCustomer(document);

    // Dispatch notification.
    await smtp.dispatchIndividualCustomerLevel1Verified(input);

    return true;
};

// Export composed resolver.
export default compose(resolve, 'verifying individual customer level 1');
