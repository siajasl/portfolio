/**
 * @fileOverview Pulls & renders channel information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import render from '../../renderers/renderChannel';

/**
 * Command execution entry point.
 */
const execute = async ({ channelId: channelID }, opts) => {
    // Pull.
    const channel = await API.clearing.getChannel({
        customerID: cache.getCustomerID(),
        channelID
    });

    // Parse.
    parsing.parseChannel(channel);

    // Render.
    await render(channel, true, true);
};

// Export command.
export default (program) => {
    program
        .command('view-settlement-channel', 'View detailed settlement channel information')
        .argument('<channelId>', 'Channel ID', parsing.parseUUID)
        .action((args, opts) => executor(execute, args, opts));
};
