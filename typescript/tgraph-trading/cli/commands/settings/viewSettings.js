/**
 * @fileOverview Pulls & renders settlement information.
 */

import { cache, executor } from '../../utils/index';
import render from '../../renderers/renderSettings';

/**
 * Command execution entry point.
 */
const execute = async (args, opts) => {
    await render(
        cache.getAssetAddresses(),
        cache.getTradingPreferences(),
        cache.getAccessFile(),
    )
};

// Export command.
export default (program) => {
    program
        .command('view-settings', 'View settings used by this application ')
        .action((args, opts) => executor(execute, args, opts));
}
