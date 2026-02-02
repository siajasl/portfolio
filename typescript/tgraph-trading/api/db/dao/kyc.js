/**
 * @fileOverview KYC related data access operations.
 */

import { createOrUpdate } from './utils';

/**
 * Adds a corporate customer (pending).
 *
 * @param {mongoose.Model} CorporateCustomer - Corporate customer dB model .
 * @param {object} obj - Corporate customer data.
 */
export const addCorporateCustomer = async ({ CorporateCustomer }, obj) => {
    return await CorporateCustomer.create(obj);
}

/**
 * Adds a level 1 (pending) individual customer.
 *
 * @param {mongoose.Model} IndividualCustomer - Individual customer dB model.
 * @param {object} obj - Level 1 individual customer data.
 */
export const addIndividualCustomerLevel1 = async ({ IndividualCustomer }, obj) => {
    return await createOrUpdate(IndividualCustomer, obj, {
        customerID: obj.customerID,
    });
}

/**
 * Promotes a level 1 individual customer to level 2 (pending).
 *
 * @param {mongoose.Model} IndividualCustomer - Individual customer dB model.
 * @param {object} obj - Level 2 individual customer data.
 */
export const addIndividualCustomerLevel2 = async ({ IndividualCustomer }, obj) => {
    return await IndividualCustomer.updateOne({
        customerID: obj.customerID,
    }, obj);
}

/**
 * Updates an individual customer.
 *
 * @param {mongoose.Model} IndividualCustomer - Individual customer dB model.
 * @param {object} obj - Updated information.
 */
export const updateIndividualCustomer = async ({ IndividualCustomer }, obj) => {
    return await IndividualCustomer.findOneAndUpdate({
        customerID: obj.customerID,
    }, obj);
}

/**
 * Returns a corporate customer matched by customer identifier.
 *
 * @param {mongoose.Model} CorporateCustomer - Corporate customer database model .
 * @param {string} customerID - customer identifier.
 */
export const getCorporateCustomer = async ({ CorporateCustomer }, customerID) => {
    return await CorporateCustomer.findOne({customerID});
};

/**
 * Returns an individual customer matched by customer identifier.
 *
 * @param {mongoose.Model} IndividualCustomer - Individual customer database model .
 * @param {string} customerID - customer identifier.
 */
export const getIndividualCustomer = async ({ IndividualCustomer }, customerID) => {
    return await IndividualCustomer.findOne({ customerID });
};

/**
* Returns customer information matched by customer identifier.
 *
 * @param {[mongoose.Model]} dbModels - Mongoose dB models.
 * @param {string} customerID - customer identifier.
 */
export const getCustomerInfo = async (dbModels, customerID) => {
    const getMapped = (customerType, details) => {
        return {
            customerID,
            type: customerType,
            verificationLevel: details.verificationLevel,
            verificationStatus: details.verificationStatus,
            verificationTxReference: details.verificationTxReference,
        }
    };

    const individual = await getIndividualCustomer(dbModels, customerID);
    if (individual) {
        return getMapped('Individual', individual);
    }

    const corporate = await getCorporateCustomer(dbModels, customerID);
    if (corporate) {
        return getMapped('Corporate', corporate);
    }
};
