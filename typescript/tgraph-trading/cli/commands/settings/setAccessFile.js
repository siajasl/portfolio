/**
 * @fileOverview Sets an asset address for usage when trading.
 */

import { cache, executor } from '../../utils/index';

/**
 * Command execution entry point.
 */
const execute = async ({ path }, opts) => {
    // TODO validate path.

    // Assign.
    cache.setAccessFile(path);

    // Persist.
    await cache.dump();
};

// Export command.
export default (program) => {
    program
        .command('set-accessfile', 'Set path to your blockchain access file')
        .argument('<path>', 'File path to your access file')
        .action((args, opts) => executor(execute, args, opts));
}
