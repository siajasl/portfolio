/**
 * @fileOverview Pulls & renders settlement list information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import render from '../../renderers/renderSettlementList';

/**
 * Command execution entry point.
 */
const execute = async (args, { status: state, symbol: assetPair }) => {
    // Pull.
    const settlements = await API.clearing.getOpenSettlements({
        customerID: cache.getCustomerID()
    });

    // Parse.
    settlements.forEach(parsing.parseSettlement);

    // Render.
    await render(settlements);
};

// Export command.
export default (program) => {
    program
        .command('view-open-settlements', 'View settlement in progress')
        .action((args, opts) => executor(execute, args, opts));
}
