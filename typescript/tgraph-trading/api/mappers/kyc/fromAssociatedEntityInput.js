/**
 * @fileOverview Maps a GQL layer struct to a dB layer struct.
 */

/**
 * Maps a GQL layer struct to a dB layer struct.
 */
export default (input) => {
    // Destructure GQL struct.
    const {
        associationType,
        contactDetails,
        isPoliticallyExposed,
        name,
        registrationNumber,
    } = input;

    // Return dB struct.
    return {
        associationType,
        contactDetails,
        isPoliticallyExposed,
        name,
        registrationNumber,
        ...mapDocuments(input)
    }
};

/**
* Maps associated parties into sub-collections.
 */
const mapDocuments = (input) => {
    const {
        certificateOfIncorporation,
        sourceOfFunds,
        sourceOfWealth
    } = input;

    const assignType = (document, documentType) => {
        if (document) {
            document.documentType = documentType;
        }
    };

    assignType(certificateOfIncorporation, 'CertificateOfIncorporation');
    assignType(sourceOfFunds, 'SourceOfFunds');
    assignType(sourceOfWealth, 'SourceOfWealth');

    return {
        certificateOfIncorporation,
        sourceOfFunds,
        sourceOfWealth,
    }
};
