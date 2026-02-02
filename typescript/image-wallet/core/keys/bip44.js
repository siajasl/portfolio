/**
 * @fileOverview Encapsulates key derivation across chains.
 */

import { isBuffer, isInteger } from 'lodash';
import * as coins from '../coins/index';
import * as exceptions from '../utils/exceptions';
import HDKey from './bip32/hdKey';

// Map of elliptic curves to seed modifiers.
const SEED_MODIFIERS = {
    'secp256k1': 'Bitcoin seed',
    'ed25519': 'ed25519 seed'
}

/**
 * Returns a entropy derived from seed by applying a derivation path algorithm.
 *
 * @param {Buffer} seed - Seed derived from an entropy source.
 * @param {String} coin - Coin symbol, e.g. AF.
 * @param {Number} account - Account identifier.
 * @param {Boolean} isExternal - Flag indicating whether account is meant to be publically revealed.
 * @param {Number} address - Address identifier.
 * @return {HDKey} keyPair - Derived key pair.
 */
export default (seed, coin, account, chain, address) => {
    // Defensive programming.
    if (isBuffer(seed) === false || seed.length != 64) {
        throw new exceptions.InvalidSeedError();
    }
    const coinInfo = coins.getBySymbol(coin);
    if (!coinInfo) {
        throw new exceptions.InvalidCoinSymbolError(coin);
    }
    if (!isInteger(account) || account < 0) {
        throw new exceptions.InvalidAccountError(account);
    }
    if (address && (!isInteger(address) || address <= 0)) {
        throw new exceptions.InvalidAddressError(address);
    }

    const path = getDerivationPath(coinInfo.index, account, chain || 0, address);

    return HDKey.create(seed, coin, path);
}

/**
 * Returns derivation path to be used to deterministically derive keys.
 */
const getDerivationPath = (coin, account, chain, address) => {
    return `m/44H/${coin}H/${account}H`;
    let path = `m/44H/${coin}H/${account}H/${chain}`;
    if (address) {
        path = `${path}/${address}`;
    }

    return path;
}
