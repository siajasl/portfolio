/**
 * @fileOverview Maps a dB layer struct to a GQL layer struct.
 */

/**
 * Maps a dB document to a GQL output type.
 */
export default (document) => {
    // Destructure dB document.
    const {
        birthDetails,
        contactDetails,
        customerID,
        email,
        expectedYearlyInvestment,
        expectedYearlySalary,
        isActingAsAgent,
        isPoliticallyExposed,
        name,
        nationality,
        principleEntity,
        principleIndividual,
        proofOfIdentity,
        proofOfResidency,
        sourceOfFunds,
        sourceOfWealth,
        verificationLevel,
        verificationStatus
    } = document;

    // Return GQL type.
    return {
        birthDetails,
        contactDetails,
        customerID,
        email,
        expectedYearlyInvestment,
        expectedYearlySalary,
        isActingAsAgent,
        isPoliticallyExposed,
        name,
        nationality,
        principleEntity,
        principleIndividual,
        proofOfIdentity,
        proofOfResidency,
        sourceOfFunds,
        sourceOfWealth,
        verificationLevel,
        verificationStatus,
    };
}
