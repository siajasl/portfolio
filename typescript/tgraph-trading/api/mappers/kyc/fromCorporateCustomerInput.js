/**
 * @fileOverview Maps a GQL layer struct to a dB layer struct.
 */

import * as _ from 'lodash';
import * as dbConstants from '../../db/constants';
import mapAssociatedEntityInput from './fromAssociatedEntityInput';
import mapAssociatedIndividualInput from './fromAssociatedIndividualInput';

/**
 * Maps a GQL layer struct to a dB layer struct.
 */
export default (input) => {
    // Destructure GQL struct.
    const {
        associatedParties,
        contactDetails,
        customerID,
        expectedYearlyInvestment,
        isPoliticallyExposed,
        name,
        registrationNumber,
        requiredDocuments,
    } = input;

    // Return dB document.
    return {
        contactDetails,
        customerID,
        expectedYearlyInvestment,
        isPoliticallyExposed,
        name,
        registrationNumber,
        verificationLevel: dbConstants.VERIFICATION_LEVEL_1,
        verificationStatus: dbConstants.VERIFICATION_STATE_PENDING,
        ...mapAssociatedParties(associatedParties),
        ...mapRequiredDocuments(requiredDocuments),
    };
};

/**
* Maps associated parties into sub-collections.
 */
const mapAssociatedParties = (parties) => {
    const assignType = (associations, associationType) => {
        if (associations) {
            associations.forEach((i) => {
                i.associationType = associationType;
            });
        }
    };

    assignType(parties.directors, 'Director');
    assignType(parties.shareholdingEntities, 'Shareholder');
    assignType(parties.shareholdingIndividuals, 'Shareholder');
    assignType(parties.signatories, 'Signatory');
    assignType(parties.uboEntities, 'UltimateBeneficiaryOwner');
    assignType(parties.uboIndividuals, 'UltimateBeneficiaryOwner');

    return {
        associatedEntities: _.map(_.without(_.concat(
            parties.shareholdingEntities,
            parties.uboEntities
        ), undefined), mapAssociatedEntityInput),
        associatedIndividuals: _.map(_.without(_.concat(
            parties.directors,
            parties.shareholdingIndividuals,
            parties.signatories,
            parties.uboIndividuals,
        ), undefined), mapAssociatedIndividualInput),
    }
};

/**
* Maps associated parties into sub-collections.
 */
const mapRequiredDocuments = (documents) => {
    const assignType = (document, documentType) => {
        if (document) {
            document.documentType = documentType;
        }
    };

    assignType(documents.articlesOfAssociation, 'ArticlesOfAssociation');
    assignType(documents.certificateOfGoodStanding, 'CertificateOfGoodStanding');
    assignType(documents.certificateOfIncorporation, 'CertificateOfIncorporation');
    assignType(documents.shareholdingStructure, 'ShareholdingStructure');
    assignType(documents.sourceOfFunds, 'SourceOfFunds');
    assignType(documents.sourceOfWealth, 'SourceOfWealth');

    return {
        documents: _.without(_.concat(
            documents.articlesOfAssociation,
            documents.certificateOfIncorporation,
            documents.certificateOfGoodStanding,
            documents.shareholdingStructure,
            documents.sourceOfFunds,
            documents.sourceOfWealth,
        ), undefined)
    }
};
