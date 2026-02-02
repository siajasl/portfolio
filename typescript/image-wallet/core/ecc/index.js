/**
 * @fileOverview Exposes supported elliptic curve set.
 */

import * as secp256k1 from './secp256k1';
import * as ed25519 from './ed25519';

// Enumeration of supported curve types.
const EllipticalCurveType = Object.freeze({
    secp256k1: 'secp256k1',
    ed25519: 'ed25519',
});

export {
    secp256k1,
    ed25519,
    EllipticalCurveType
};
