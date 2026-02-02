/**
 * @fileOverview Wraps a hierarchical deterministic key derivation path.
 */

// Limit beyond which an index is considered to be 'hardened'.
const HIGHEST_BIT = 0x80000000;

// Symbols used in BIP32 paths to indicate an index that requires 'hardening'.
const HARDENING_TOKENS = ["h", "'"];

// Token used to seperate a key derivation path.
const PATH_SEPERATOR = '/';

/**
 * Wraps an element within a key derivation path.
 * @constructor
 * @param {String} path - A key derivation path, e.g. m/44'/60'/0'/0/0.
 */
export class HDPath {
    constructor(path) {
        // TODO: validate path against a regular expression
        // isString(path) && path.match(/^(m\/)?(\d+'?\/)*\d+'?$/) !== null;

        // Derivation path.
        this.path = path;

        // Derivation nodes.
        const nodes = path.toLowerCase().split(PATH_SEPERATOR);
        this.nodes = nodes.map((element, depth) => {
            return new HDPathNode(this, element, depth);
        });

        // So-called root node, i.e. node that represents a master key.
        this.root = this.nodes[0];
    }
}

/**
 * Wraps a node within a key derivation path.
 * @constructor
 * @param {HDPath} path - Associated derivation path.
 * @param {String} element - An element within path.
 * @param {Number} depth - Depth within derivation path.
 */
class HDPathNode {
    constructor(path, element, depth) {
        // Derivation depth, 0 for master, 1 for 1st child ... etc.
        this.depth = depth;

        // Element within associated derivation path.
        this.element = element;

        // Flag indicating whether node requires hardening.
        this.isHardened = HARDENING_TOKENS.includes(element.slice(-1));

        // Derivation index.
        this.index = this.isHardened ? parseInt(element) + HIGHEST_BIT :
                                       parseInt(element);

        // Derivation index buffer for downstream ops.
        this.indexBuffer = Buffer.allocUnsafe(4);
        this.indexBuffer.writeUInt32BE(this.index, 0);

        // Associated derivation path.
        this.path = path;
    }

    /**
     * Gets flag indicating whether node is the root within a derivation path.
     */
    get isRoot() {
        return this.depth === 0;
    }
}
