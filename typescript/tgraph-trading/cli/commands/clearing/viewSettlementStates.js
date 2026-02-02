/**
 * @fileOverview Pulls & renders settlement list information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import { SettlementStateEnum } from '../../utils/enums';
import render from '../../renderers/renderSettlementStates';

/**
 * Command execution entry point.
 */
const execute = async (args, opts) => {
    await render(Object.keys(SettlementStateEnum));
};

// Export command.
export default (program) => {
    program
        .command('view-settlement-states', 'View the various states through which a settlement may pass through')
        .action((args, opts) => executor(execute, args, opts));
}
