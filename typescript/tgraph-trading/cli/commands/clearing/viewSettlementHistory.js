/**
 * @fileOverview Pulls & renders settlement list information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import { AssetPairEnum } from '../../utils/enums';
import render from '../../renderers/renderSettlementList';

/**
 * Command execution entry point.
 */
const execute = async (args, { state, symbol: assetPair }) => {
    // Pull.
    const settlements = await API.clearing.getSettlementList({
        assetPair: assetPair || AssetPairEnum.ETHBTC,
        customerID: cache.getCustomerID(),
        state
    });

    // Parse.
    settlements.forEach(parsing.parseSettlement);

    // Render.
    await render(settlements);
};

// Export command.
export default (program) => {
    program
        .command('view-settlement-history', 'View your settlement history')
        .option('--state <state>', 'Current settlement state', parsing.parseSettlementState)
        .option('--symbol <symbol>', 'Market of asset pair - see view-asset-pairs', parsing.parseAssetPair)
        .action((args, opts) => executor(execute, args, opts));
}
