/**
 * @fileOverview One time API setup script.
 */

import * as logger from '../../utils/logging';
import * as dbEngine from '../../db/engine';

/**
* @fileOverview Executes API setup procedures.
 */
export default async () => {
    logger.logInfo('initialising dB ...');

    // Set db engine.
    await dbEngine.initialise();
    const dbe = await dbEngine.getEngine('mutation');

    // Destructure config file.
    const { assets, exchanges } = require('../config.json');

    // Upsert supported assets.
    for (const { symbol, decimals } of assets) {
        await dbe.upsertAsset({
            symbol,
            decimals
        });
    }

    // Upsert supported exchanges.
    for (const {
        assetPairs,
        commissionPercentage,
        htlcTimeouts,
        matchingAlgorithm,
        name,
        uid,
        symbol
    } of exchanges) {
        await dbe.upsertExchange({
            assetPairs,
            commissionPercentage,
            htlcTimeouts,
            exchangeID: uid,
            matchingAlgorithm,
            name,
            symbol
        });
    }
};
