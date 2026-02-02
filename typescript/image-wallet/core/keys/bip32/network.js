/**
 * @fileOverview Encapsulates network specific metadata leveraged during derivation.
 */

import { BigNumber as BN } from 'bignumber.js';
import { ed25519, secp256k1 } from '../../ecc/index';
import { BTC, getBySymbol as getCoinInfo } from '../../coins/index';

/**
 * Encapsulates a network's hd key derivation metadata.
 * @constructor
 * @param {Bip32Info} bip32 - A network's bip32 related metadata.
 * @param {String} curve - A network's elliptical curve related metadata.
 * @param {WifInfo} wif - A network's wallet import format related metadata.
 */
export class Network {
    constructor(bip32, curve, wif) {
        this.bip32 = bip32;
        this.curve = curve;
        this.wif = wif;
    }

    /**
     * Returns coin specific network information required for key derivation.
     * @param {String} symbol - Supported coin trading symbol, e.g. SHU.
     */
    static create (symbol) {
        const { bip32, curve, wif } = getCoinInfo(symbol);

        return new Network(
            new Bip32Info(bip32 || BTC.bip32),
            new EllipticalCurveInfo(curve),
            new WifInfo(wif || BTC.wif),
        )
    }
}

/**
 * Encapsulates a network's bip32 versions metadata.
 * @constructor
 * @param {UInt32} private - Version associated with private network.
 * @param {UInt32} public - Version associated with public network.
 */
class Bip32Info {
    constructor ({ versions }) {
        this.versions = versions;
    }
}

/**
 * Encapsulates elliptical curve information.
 * @constructor
 * @param {String} type - Type of elliptical curve.
 */
export class EllipticalCurveInfo {
    constructor(type) {
        this.type = type;
        switch (type) {
            case 'ed25519':
                this.algo = ed25519;
                this.getPublicKey = ed25519.getPublicKey;
                this.order = null;
                this.seedModifier = 'ed25519 seed';
                break;
            case 'secp256k1':
                this.algo = secp256k1;
                this.getPublicKey = secp256k1.getPublicKeyFromPrivate;
                this.order = BN(secp256k1.order);
                this.seedModifier = 'Bitcoin seed';
                break;
        }
        this.seedModifierBuffer = Buffer.from(this.seedModifier, 'utf8');
    }
}

/**
 * Encapsulates a network's wallet import information metadata.
 * @constructor
 * @param {UInt8} version - WIF version associated with network.
 */
class WifInfo {
    constructor ({ version }) {
        this.version = version;
    }
}
