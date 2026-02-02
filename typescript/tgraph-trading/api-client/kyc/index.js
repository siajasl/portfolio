/**
 * @fileOverview Sub-package entry point.
 */

import EventEmitter from '../utils/events';
import * as inputs from './inputs/index';

import getCorporateCustomer from './getCorporateCustomer';
import getCustomerInfo from './getCustomerInfo';
import getIdentityVerificationInitiationData from './getIdentityVerificationInitiationData';
import getIdentityVerificationStatus from './getIdentityVerificationStatus';
import getIndividualCustomer from './getIndividualCustomer';

import registerCorporateCustomer from './doRegisterCorporateCustomer';
import registerIndividualCustomerLevel1 from './doRegisterIndividualCustomerLevel1';
import registerIndividualCustomerLevel2 from './doRegisterIndividualCustomerLevel2';
import rejectIndividualCustomerLevel1 from './doRejectIndividualCustomerLevel1';
import verifyIndividualCustomerLevel1 from './doVerifyIndividualCustomerLevel1';

// Instantiate sub-package specific event emitter.
const events = new EventEmitter('kyc');

export {
    // event emitter
    events,

    // input types passed into endpoints
    inputs,

    // queries
    getCorporateCustomer,
    getCustomerInfo,
    getIdentityVerificationInitiationData,
    getIdentityVerificationStatus,
    getIndividualCustomer,

    // mutations
    registerCorporateCustomer,
    registerIndividualCustomerLevel1,
    registerIndividualCustomerLevel2,
    rejectIndividualCustomerLevel1,
    verifyIndividualCustomerLevel1,
};
