/**
 * @fileOverview Returns Jumio verification initiation data.
 */

import compose from '../shared/resolvers/factory';
import * as idVerifier from './idVerifier/index'

/**
 * Returns identity verifier's verification state.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    const { verificationTxReference, customerID } = input;
    const customer = await dbe.getIndividualCustomer(customerID);
    if (!customer) {
        throw new Error(`Unknown customer identifier: ${customerID}`)
    }

    const {
        birthDetails: {
            date: dateOfBirth
        },
        name : {
            last: lastName
        }
    } = customer;

    return idVerifier.getStatus(verificationTxReference, lastName, dateOfBirth);
};

// Export composed resolver.
export default compose(resolve, `get identity verification state`);
