/**
 * @fileOverview Represents set of corporate documents that are required by law.
 */

import { chance } from '../../utils/testing';
import { mapObject } from '../../utils/gql/index';
import * as Document from './Document';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        articlesOfAssociation:      ${mapObject(obj.articlesOfAssociation, Document)}
        certificateOfGoodStanding:  ${mapObject(obj.certificateOfGoodStanding, Document)}
        certificateOfIncorporation: ${mapObject(obj.certificateOfIncorporation, Document)}
        shareholdingStructure:      ${mapObject(obj.shareholdingStructure, Document)}
        sourceOfFunds:              ${mapObject(obj.sourceOfFunds, Document)}
        sourceOfWealth:             ${mapObject(obj.sourceOfWealth, Document)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = () => {
    return {
        articlesOfAssociation:      Document.create('Articles Of Association'),
        certificateOfGoodStanding:  Document.create('Certificate Of Good Standing'),
        certificateOfIncorporation: Document.create('Certificate Of Incorporation'),
        shareholdingStructure:      Document.create('Shareholding Structure'),
        sourceOfFunds:               Document.create('Proof Of Funds'),
        sourceOfWealth:              Document.create('Proof Of Wealth'),
    };
}
