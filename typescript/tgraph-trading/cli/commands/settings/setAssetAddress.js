/**
 * @fileOverview Sets an asset address for usage when trading.
 */

import { cache, executor, parsing } from '../../utils/index';

/**
 * Command execution entry point.
 */
const execute = async ({ address, symbol }, opts) => {
    // Assign.
    cache.setAssetAddress(symbol, address);

    // Persist.
    await cache.dump();
};

// Export command.
export default (program) => {
    program
        .command('set-address', 'Set asset address to be used during trade settlement')
        .argument('<symbol>', 'Asset type symbol', parsing.parseAsset)
        .argument('<address>', 'Ledger address')
        .action((args, opts) => executor(execute, args, opts));
}
