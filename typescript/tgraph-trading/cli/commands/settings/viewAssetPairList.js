/**
 * @fileOverview Pulls & renders asset pair information.
 */

import { executor } from '../../utils/index';
import { AssetPairEnum } from '../../utils/enums';
import render from '../../renderers/renderAssetPairs';

/**
 * Command execution entry point.
 */
const execute = async ({ address, symbol }, opts) => {
    await render(Object.keys(AssetPairEnum).sort());
};

// Export command.
export default (program) => {
    program
        .command('view-asset-pairs', 'Displays list of available asset pairs')
        .action((args, opts) => executor(execute, args, opts));
}
