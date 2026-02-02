/**
 * @fileOverview Derives a so-called extended key.
 */

import { EllipticalCurveType } from '../../ecc/index';

/**
 * Returns an extended key.
 * @param {HDKey} hdKey - An HD key pair.
 * @param {Buffer} version - Network's bip32 version.
 * @param {Buffer} key - Either the hd key's public or it's private key.
 * @return {Buffer} Extended key.
 */
export default (hdKey) => {

    console.log(hdKey);


};
