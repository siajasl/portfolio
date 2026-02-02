/**
 * @fileOverview Wraps command execution so as to simplify the wrapped command.
 */

import * as session from './session';
import { renderError } from '../renderers/utils';

/**
 * Wraps command execution so as to simplify the wrapped command.
 *
 * @param {Function} cmd - Command to be executed.
 * @param {Object} args - Command arguments.
 * @param {Object} opts - Command options.
 */
export default async (cmd, args, opts) => {
    try {
        await session.init();
        await cmd(args, opts);
        process.exit(0);
    }
    catch (err) {
        await renderError(err);
        process.exit(1);
    }
}
