/**
 * @fileOverview Pulls & renders asset pair information.
 */

import { executor } from '../../utils/index';
import { ExchangeDescriptionEnum } from '../../utils/enums';
import render from '../../renderers/renderExchanges';

/**
 * Command execution entry point.
 */
const execute = async ({ address, symbol }, opts) => {
    const data = Object.keys(ExchangeDescriptionEnum).map(
        i => `${i} :: ${ExchangeDescriptionEnum[i]}`
    );

    await render(data);
};

// Export command.
export default (program) => {
    program
        .command('view-exchanges', 'Displays list of supported exchanges')
        .action((args, opts) => executor(execute, args, opts));
}
