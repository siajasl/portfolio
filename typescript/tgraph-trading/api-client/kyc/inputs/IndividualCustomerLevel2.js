/**
 * @fileOverview Represents level 2 individual customer details.
 */

import { getBoolean, getHash, getNumber } from '../../utils/testing';
import { mapArray, mapObject, mapString } from '../../utils/gql/index';
import * as AssociatedEntity from './AssociatedEntity';
import * as AssociatedIndividual from './AssociatedIndividual';
import * as Document from './Document';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        customerID:               ${mapString(obj.customerID)}
        expectedYearlyInvestment: ${obj.expectedYearlyInvestment}
        expectedYearlySalary:     ${obj.expectedYearlySalary}
        isActingAsAgent:          ${obj.isActingAsAgent}
        isPoliticallyExposed:     ${obj.isPoliticallyExposed}
        principleEntity:          ${mapObject(obj.principleEntity, AssociatedEntity)}
        principleIndividual:      ${mapObject(obj.principleIndividual, AssociatedIndividual)}
        proofOfResidency:         ${mapArray(obj.proofOfResidency, Document)}
        sourceOfFunds:            ${mapObject(obj.sourceOfFunds, Document)}
        sourceOfWealth:           ${mapObject(obj.sourceOfWealth, Document)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = (customerID) => {
    // Simulate agents.
    let principleEntity = null;
    let principleIndividual = null;
    const isActingAsAgent = getBoolean();
    if (isActingAsAgent) {
        const isPrincipleEntity = getBoolean();
        if (isPrincipleEntity) {
            principleEntity = AssociatedEntity.create();
        } else {
            principleIndividual = AssociatedIndividual.create();
        }
    };

    return {
        expectedYearlyInvestment: getNumber(15000, 1500000),
        expectedYearlySalary:     getNumber(15000, 1500000),
        customerID:               customerID || getHash(),
        isActingAsAgent:          isActingAsAgent,
        isPoliticallyExposed:     getBoolean(),
        principleEntity:          principleEntity,
        principleIndividual:      principleIndividual,
        proofOfResidency:         ['Utility Bill', 'Rental contract'].map((i) => Document.create(i)),
        sourceOfFunds:            Document.create("Bank statement"),
        sourceOfWealth:           Document.create("Accountant's letter"),
    };
}
