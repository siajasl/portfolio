/**
 * @fileOverview A hierarchically deterministic key.
 */

import * as wifCodec from '../wif';
import { getBuffer } from '../../utils/conversion';
import { Network } from './network';
import { HDPath } from './hdPath';
import hash160 from '../../hashing/hash160';
import getAddress from './getAddress';
import getChildKey from './getChildKey';
import getExtendedKeyBase58 from './getExtendedKeyBase58';
import getMasterKey from './getMasterKey';

// Default derivation path = master node.
const DEFAULT_PATH = 'm';

// Parent fingerprint for a root node.
const ROOT_PARENT_FINGERPRINT = Buffer.alloc(4, 0);

/**
 * Universal private key derivation from master private key.
 * @constructor
 * @param {NetworkInfo} network - Network metadata required during key derivation.
 * @param {Buffer} privateKey - Derived private key.
 * @param {Buffer} chainCode - Derived chain code.
 * @param {HDKey} parent - Parent key.
 * @param {HDPathNode} node - Master node within a derivation path.
 */
export default class HDKey {
    constructor(network, privateKey, chainCode, parent, node) {
        // Derived chain code.
        this.chainCode = chainCode;

        // Network information.
        this.network = network;

        // Associated derivation path node.
        this.node = node;

        // Parent key in hierarchy.
        this.parent = parent;

        // Derived private key.
        this.privateKey = privateKey;

        // JIT derived base58 encoded extended private key.
        this._extendedPrivateKey = null;

        // JIT derived base58 encoded extended public key.
        this._extendedPublicKey = null;

        // JIT derived fingerprint.
        this._fingerprint = null;

        // JIT derived identifier.
        this._identifier = null;

        // JIT derived public key.
        this._publicKey = null;

        // JIT derived wallet import format key.
        this._privateKeyAsWIF = null;
    }

    /**
     * Gets Derivation depth, 0 for master, 1 for 1st child ... etc.
     */
    get depth() {
        return this.node.depth;
    }

    /**
     * Gets so-called extended private key.
     */
    get extendedPrivateKey() {
        if (this.privateKey && this._extendedPrivateKey === null) {
            this._extendedPrivateKey = getExtendedKeyBase58(this, this.network.bip32.versions.private, this.privateKey);
        }
        return this._extendedPrivateKey;
    }

    /**
     * Gets so-called extended private key in hexadecimal string format.
     */
    get extendedPrivateKeyAsHexString() {
        return this.extendedPrivateKey.toString('hex')
    }

    /**
     * Gets so-called extended public key.
     */
    get extendedPublicKey() {
        if (this.publicKey && this._extendedPublicKey === null) {
            this._extendedPublicKey = getExtendedKeyBase58(this, this.network.bip32.versions.public, this.publicKey);
        }
        return this._extendedPublicKey;
    }

    /**
     * Gets so-called extended public key in hexadecimal string format.
     */
    get extendedPublicKeyAsHexString() {
        return this.extendedPublicKey.toString('hex')
    }

    /**
    * Gets associated fingerprint.
    */
    get fingerprint() {
        return this.identifier.slice(0, 4);
    }

    /**
     * Gets associated identifier.
     */
    get identifier() {
        if (this._identifier === null) {
            this._identifier = hash160(this.publicKey);
        }
        return this._identifier;
    }

    /**
     * Gets flag indicating whether the key is considered neutured, i.e. unfit for signing.
     */
    get isNeutered() {
        return this.privateKey === null;
    }

    /**
     * Gets parent fingerprint.
     */
    get parentFingerprint() {
        return this.parent ? this.parent.fingerprint : ROOT_PARENT_FINGERPRINT;
    }

    /**
     * Gets associated private key in hexadecimal string format.
     */
    get privateKeyAsHexString() {
        return this.privateKey.toString('hex');
    }

    /**
     * Gets associated private key in wallet import format.
     */
    get privateKeyAsWIF() {
        if (this._privateKeyAsWIF === null) {
            this._privateKeyAsWIF = wifCodec.encode(this.network.wif.version, this.privateKey, true);
        }
        return this._privateKeyAsWIF;
    }

    /**
     * Gets associated private key in wallet import format in hexadecimal string format.
     */
    get privateKeyAsWIFHexString() {
        return this.privateKeyAsWIF.toString('hex');
    }

    /**
     * Gets associated public key.
     */
    get publicKey() {
        if (this._publicKey === null) {
            this._publicKey  = this.network.curve.getPublicKey(this.privateKey);
        }
        return this._publicKey;
    }

    /**
     * Gets associated public key in hexadecimal string format.
     */
    get publicKeyAsHexString() {
        return this.publicKey.toString('hex');
    }

    /**
     * Returns a master key for a particular network.
     * @param {Buffer} seed - 64 byte master seed (derived from 32 bytes of entropy).
     * @param {String} symbol - Coin trading symbol, e.g. BTC.
     * @param {String} derivationPath - Derivation path, e.g. m/44'/60'/0'/0/0.
     */
    static create (seed, symbol, derivationPath) {
        // Set derivation path.
        const path = new HDPath(derivationPath || DEFAULT_PATH);

        // Set master key.
        let hdKey = HDKey.createMaster(seed, symbol, path.root);

        // Set child key(s).
        const nodes = path.nodes.slice(1);
        for (const node of nodes) {
            hdKey = HDKey.createChild(hdKey, node);
        }

        return hdKey;
    }

    /**
     * Returns a master key derived from a seed & network symbol.
     * @param {Buffer} seed - 64 byte master seed (derived from 32 bytes of entropy).
     * @param {String} symbol - Coin trading symbol, e.g. BTC.
     * @param {HDPathNode} node - Master node within a derivation path.
     */
    static createMaster (seed, symbol, node) {
        // Convert seed to a buffer.
        seed = getBuffer(seed);

        // Set network information.
        const network = Network.create(symbol);

        // Derive key/chain-code.
        const { key, chainCode } = getMasterKey(seed, network);

        return new HDKey(network, key, chainCode, null, node);
    }

    /**
     * Returns a child key derived from a parent key.
     * @param {HDKey} parent - Parent HD key.
     * @param {HDPathNode} node - Node within a derivation path.
     */
    static createChild (parent, node) {
        const { network } = parent;
        const { key, chainCode } = getChildKey(parent, node);

        return new HDKey(network, key, chainCode, parent, node);
    }
}
