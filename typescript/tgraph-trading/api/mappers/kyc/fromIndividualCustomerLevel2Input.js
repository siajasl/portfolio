/**
 * @fileOverview Maps a GQL layer struct to a dB layer struct.
 */

import * as dbConstants from '../../db/constants';

/**
 * Maps a GQL layer struct to a dB layer struct.
 */
export default (input) => {
    return {
        ...input,
        verificationLevel: dbConstants.VERIFICATION_LEVEL_2,
        verificationStatus: dbConstants.VERIFICATION_STATE_PENDING
    }
};
