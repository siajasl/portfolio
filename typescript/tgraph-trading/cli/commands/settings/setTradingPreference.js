/**
 * @fileOverview Sets a trading preference for usage when trading.
 */

import { cache, executor } from '../../utils/index';

/**
 * Command execution entry point.
 */
const execute = async ({ name, value }, opts) => {
    // TODO: validate preference

    // Assign.
    cache.setTradingPreference(name, value);

    // Persist.
    await cache.dump();
};

// Export command.
export default (program) => {
    program
        .command('set-preference', 'Set a trading preference')
        .argument('<name>', 'Trading preference name')
        .argument('<value>', 'Trading preference value')
        .action((args, opts) => executor(execute, args, opts));
}
