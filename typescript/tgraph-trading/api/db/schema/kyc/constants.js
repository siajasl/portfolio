/**
 * @fileOverview Database constants.
 */

// Document encoding types.
export const DOCUMENT_ENCODING_BASE64 = 'Base64';
export const DOCUMENT_ENCODING = [
    DOCUMENT_ENCODING_BASE64
];

// Identity card types.
export const IDENTITY_CARD_DRIVING_LICENCE = "DrivingLicence";
export const IDENTITY_CARD_PASSPORT = "Passport";
export const IDENTITY_CARD_PERMIT = "ResidencePermit";
export const IDENTITY_CARD = [
    IDENTITY_CARD_DRIVING_LICENCE,
    IDENTITY_CARD_PASSPORT,
    IDENTITY_CARD_PERMIT
];

// Customer onboarding process verification levels.
export const VERIFICATION_LEVEL_1 = 'Level1';
export const VERIFICATION_LEVEL_2 = 'Level2';
export const VERIFICATION_LEVEL = [
    VERIFICATION_LEVEL_1,
    VERIFICATION_LEVEL_2,
];

// Customer onboarding process verification state.
export const VERIFICATION_STATE_VERIFIED = 'Verified';
export const VERIFICATION_STATE_PENDING = 'Pending';
export const VERIFICATION_STATE_REJECTED = 'Rejected';
export const VERIFICATION_STATE = [
    VERIFICATION_STATE_VERIFIED,
    VERIFICATION_STATE_PENDING,
    VERIFICATION_STATE_REJECTED,
];
