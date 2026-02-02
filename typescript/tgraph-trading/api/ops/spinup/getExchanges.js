/**
 * @fileOverview Returns set of supported exchanges.
 */

// Trading platform engine.
import { ClearingEngine, TradingEngine } from '../../utils/imports';

/**
 * Returns meta-data regarding setof supported exchanges.
 */
export default async (dbe) => {
    const assets = await getAssets(dbe);

    return await getExchanges(dbe, assets);
};

/**
 * Returns set of supported assets.
 */
const getAssets = async (dbe) => {
    // Pull from dB.
    const config = await dbe.getAssets();

    // Map to domain model.
    return config.map(({ symbol, decimals }) => new TradingEngine.Asset(symbol, decimals));
}

/**
 * Returns set of supported exchanges.
 */
const getExchanges = async (dbe, assets) => {
    // Pull from dB.
    const config = await dbe.getExchanges();

    // Map to domain model.
    return config.map(i => getExchange(assets, i));
}

/**
 * Returns a supported exchange.
 */
const getExchange = (assets, {
    assetPairs,
    commissionPercentage,
    exchangeID,
    htlcTimeouts,
    matchingAlgorithm,
    name,
    symbol
}) => {
    // Build list of supported assets.
    assets = assetPairs.reduce((out, pair) => {
        return out.concat([
            assets.find(i => i.symbol === pair.slice(0, 3)),
            assets.find(i => i.symbol === pair.slice(3))
        ]);
    }, []);

    // Build list of supported asset pairs.
    assetPairs = assetPairs.map((pair) => {
        return new TradingEngine.AssetPair(
            assets.find(i => i.symbol === pair.slice(0, 3)),
            assets.find(i => i.symbol === pair.slice(3))
        );
    });

    // Return exchange instance.
    return new TradingEngine.Exchange({
        assets: new Set(assets),
        assetPairs,
        commissionPercentage,
        htlcTimeouts: new ClearingEngine.HtlcTimeouts(htlcTimeouts),
        id: exchangeID,
        matchingAlgorithm,
        name,
        symbol
    });
}
