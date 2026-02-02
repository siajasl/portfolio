/**
 * @fileOverview Maps a GQL layer struct to a dB layer struct.
 */

/**
 * Maps a GQL layer struct to a dB layer struct.
 */
export default (input) => {
    // Destructure GQL struct.
    const output = {
        ...input
    }

    // Assign document types.
    setDocumentTypes(
        output.proofOfEmployment,
        output.proofOfResidency,
        output.sourceOfFunds,
        output.sourceOfWealth
    );

    // Return dB struct.
    return output;
};

/**
* Sets required document types.
 */
const setDocumentTypes = (
    proofOfEmployment,
    proofOfResidency,
    sourceOfFunds,
    sourceOfWealth
) => {
    const assignType = (documents, documentType) => {
        if (documents) {
            documents.forEach((document) => {
                if (document) {
                    document.documentType = documentType;
                }
            });
        }
    };

    assignType([proofOfEmployment], 'ProofOfEmployment');
    assignType(proofOfResidency, 'ProofOfResidency');
    assignType([sourceOfFunds], 'SourceOfFunds');
    assignType([sourceOfWealth], 'SourceOfWealth');
};
