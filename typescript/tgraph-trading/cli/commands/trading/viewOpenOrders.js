/**
 * @fileOverview Pulls & renders order list information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import render from '../../renderers/renderOrderList';

/**
 * Command execution entry point.
 */
const execute = async (args, { assetPair }) => {
    // Set payload.
    const payload = {
        customerID: cache.getCustomerID()
    }
    if (assetPair) {
        payload['assetPair'] = assetPair;
    }

    // Pull.
    const orders = await API.trading.getOpenOrders(payload);

    // Parse.
    orders.forEach(parsing.parseOrder);

    // Render.
    await render(orders);
};

/**
 * Export command to application.
 */
export default (program) => {
    program
        .command('view-open-orders', 'View your open orders')
        .option('--symbol <assetPair>', 'Market of asset pair', parsing.parseAssetPair)
        .action((args, opts) => executor(execute, args, opts));
}
