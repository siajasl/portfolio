/**
 * @fileOverview KYC/AML related endpoint resolvers.
 */

import registerCorporateCustomer from './doRegisterCorporateCustomer';
import registerIndividualCustomerLevel1 from './doIndividualCustomerLevel1_Register';
import registerIndividualCustomerLevel2 from './doIndividualCustomerLevel2_Register';
import rejectIndividualCustomerLevel1 from './doIndividualCustomerLevel1_Reject';
import verifyIndividualCustomerLevel1 from './doIndividualCustomerLevel1_Verify';
import getCorporateCustomer from './getCorporateCustomer';
import getCustomerInfo from './getCustomerInfo';
import getIdentityVerificationInitiationData from './getIdentityVerificationInitiationData';
import getIdentityVerificationStatus from './getIdentityVerificationStatus';
import getIndividualCustomer from './getIndividualCustomer';


// Mutation set.
export const mutation = {
    registerIndividualCustomerLevel1,
    registerIndividualCustomerLevel2,
    rejectIndividualCustomerLevel1,
    registerCorporateCustomer,
    verifyIndividualCustomerLevel1,
}

// Query set.
export const query = {
    getCorporateCustomer,
    getCustomerInfo,
    getIdentityVerificationInitiationData,
    getIdentityVerificationStatus,
    getIndividualCustomer,
}

// Super set.
export const all = {
    getCorporateCustomer,
    getCustomerInfo,
    getIdentityVerificationInitiationData,
    getIndividualCustomer,
    getIdentityVerificationStatus,
    registerIndividualCustomerLevel1,
    registerIndividualCustomerLevel2,
    rejectIndividualCustomerLevel1,
    registerCorporateCustomer,
    verifyIndividualCustomerLevel1,
}
