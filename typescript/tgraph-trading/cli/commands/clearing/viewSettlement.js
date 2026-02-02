/**
 * @fileOverview Pulls & renders settlement information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import render from '../../renderers/renderSettlement';

/**
 * Command execution entry point.
 */
const execute = async ({ settlementId: settlementID }, opts) => {
    // Pull.
    const settlement = await API.clearing.getSettlement({
        customerID: cache.getCustomerID(),
        settlementID,
    });

    // Parse.
    parsing.parseSettlement(settlement);

    // Render.
    await render(settlement);
};

// Export command.
export default (program) => {
    program
        .command('view-settlement', 'View detailed settlement information')
        .argument('<settlementId>', 'Settlement ID', parsing.parseUUID)
        .action((args, opts) => executor(execute, args, opts));
}
