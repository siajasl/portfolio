/**
 * @fileOverview Returns Jumio verification initiation data.
 */

import compose from '../shared/resolvers/factory';
import * as idVerifier from './idVerifier/index'

/**
 * Returns identity verifier's initiate verification API endpoint response.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    const { customerID } = input;

    return idVerifier.initiate(customerID);
};

// Export composed resolver.
export default compose(resolve, `get verification initiatation data`);
