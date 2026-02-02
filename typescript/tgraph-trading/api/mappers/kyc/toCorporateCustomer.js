/**
 * @fileOverview Maps a dB layer struct to a GQL layer struct.
 */

import * as _ from 'lodash';

/**
 * Maps a dB document to a GQL output type.
 */
export default (document) => {
    // Destructure dB document.
    const {
        contactDetails,
        customerID,
        expectedYearlyInvestment,
        isPoliticallyExposed,
        name,
        registrationNumber,
        associatedEntities,
        associatedIndividuals,
        documents,
        verificationLevel,
        verificationStatus
    } = document;

    // Return GQL type.
    return {
        contactDetails,
        customerID,
        expectedYearlyInvestment,
        isPoliticallyExposed,
        name,
        registrationNumber,
        verificationLevel,
        verificationStatus,
        associatedParties: mapAssociatedParties(associatedIndividuals,
                                                associatedEntities),
        requiredDocuments: mapRequiredDocuments(documents),
    }
};

/**
 * Maps associated parties from individuals/entities.
 */
const mapAssociatedParties = (associatedIndividuals, associatedEntities) => {
    const findMany = (parties, associationType) => {
        return _.filter(parties, (i) => {
            return i.associationType === associationType;
        });
    }

    return {
        directors: findMany(associatedIndividuals, 'Director'),
        shareholdingEntities: findMany(associatedEntities, 'Shareholder'),
        shareholdingIndividuals: findMany(associatedIndividuals, 'Shareholder'),
        signatories: findMany(associatedIndividuals, 'Signatory'),
        uboEntities: findMany(associatedEntities, 'UltimateBeneficiaryOwner'),
        uboIndividuals: findMany(associatedIndividuals, 'UltimateBeneficiaryOwner'),
    }
};

/**
 * Maps required documents container.
 */
const mapRequiredDocuments = (documents) => {
    const findOne = (documentType) => {
        return _.find(documents, (i) => {
            return i.documentType === documentType;
        });
    }

    const result = {
        articlesOfAssociation: findOne('ArticlesOfAssociation'),
        certificateOfGoodStanding: findOne('CertificateOfGoodStanding'),
        certificateOfIncorporation: findOne('CertificateOfIncorporation'),
        shareholdingStructure: findOne('ShareholdingStructure'),
        sourceOfFunds: findOne('SourceOfFunds'),
        sourceOfWealth: findOne('SourceOfWealth'),
    };

    return result;
};
