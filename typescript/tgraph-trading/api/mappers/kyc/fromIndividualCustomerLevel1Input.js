/**
 * @fileOverview Maps a GQL layer struct to a dB layer struct.
 */

import * as dbConstants from '../../db/constants';

/**
 * Maps a GQL layer struct to a dB layer struct.
 */
export default (input) => {
    // Initialise document.
    const document = {
        ...input,
        verificationLevel: dbConstants.VERIFICATION_LEVEL_1,
        verificationStatus: dbConstants.VERIFICATION_STATE_PENDING,
    }

    // Set association types.
    if (document.principleEntity) {
        document.principleEntity.associationType = 'Principle';
    }
    if (document.principleIndividual) {
        document.principleIndividual.associationType = 'Principle';
    }

    return document;
};
