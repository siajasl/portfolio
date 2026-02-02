/**
 * @fileOverview Cancels an order.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import { renderMessage } from '../../renderers/utils';

/**
 * Command execution entry point.
 */
const execute = async ({ orderId: orderID }, opts) => {
    // Push.
    const reponse = await API.trading.cancelOrder({
        customerID: cache.getCustomerID(),
        orderID
    });

    // Render.
    await renderMessage(`Order ${orderID} cancelled`);
};

/**
 * Export command to application.
 */
export default (program) => {
    program
        .command('cancel-order', 'Cancels a previously submitted order')
        .argument('<orderId>', 'Order ID', parsing.parseUUID)
        .action((args, opts) => executor(execute, args, opts));
}
