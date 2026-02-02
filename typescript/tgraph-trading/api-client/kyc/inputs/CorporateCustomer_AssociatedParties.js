/**
 * @fileOverview Represents various parties associated with a corporation in some way.
 */

import { chance } from '../../utils/testing';
import { mapArray } from '../../utils/gql/index';
import * as AssociatedIndividual from './AssociatedIndividual';
import * as AssociatedEntity from './AssociatedEntity';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        directors:               ${mapArray(obj.directors, AssociatedIndividual)}
        shareholdingEntities:    ${mapArray(obj.shareholdingEntities, AssociatedEntity)}
        shareholdingIndividuals: ${mapArray(obj.shareholdingIndividuals, AssociatedIndividual)}
        signatories:             ${mapArray(obj.signatories, AssociatedIndividual)}
        uboEntities:             ${mapArray(obj.uboEntities, AssociatedEntity)}
        uboIndividuals:          ${mapArray(obj.uboIndividuals, AssociatedIndividual)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = () => {
    return {
        directors:               [0, 1].map((_) => AssociatedIndividual.create()),
        shareholdingEntities:    [0, 1].map((_) => AssociatedEntity.create()),
        shareholdingIndividuals: [0, 1].map((_) => AssociatedIndividual.create()),
        signatories:             [0, 1].map((_) => AssociatedIndividual.create()),
        uboEntities:             [0, 1].map((_) => AssociatedEntity.create()),
        uboIndividuals:          [0, 1].map((_) => AssociatedIndividual.create()),
    };
}
