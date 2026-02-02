import * as Utils from 'web3-utils';

/**
 * Generates a secret for a BTC channel.
 */
const generateBTC = () => {
    // cut the prefix '0x' from hex string.
    return Utils.randomHex(32).replace('0x', '');
}

/**
 * Generates a secret for an ETH channel.
 */
const generateETH = () => {
    // cut the prefix '0x' from hex string
    return Utils.randomHex(32).replace('0x', '');
}

// Map: asset type <-> generator.
const GENERATORS = {
    'BTC': generateBTC,
    'ETH': generateETH,
}

/**
 * Generates a secret for an asset channel.
 */
export default (assetType) => {
    return GENERATORS[assetType]();
}
