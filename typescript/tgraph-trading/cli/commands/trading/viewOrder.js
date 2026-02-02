/**
 * @fileOverview Pulls & renders order information.
 */

import { cache, executor, parsing } from '../../utils/index';
import { API } from '../../utils/imports';
import render from '../../renderers/renderOrder';

/**
 * Command execution entry point.
 */
const execute = async ({ orderId: orderID }, opts) => {
    // Pull.
    const order = await API.trading.getOrder({
        customerID: cache.getCustomerID(),
        orderID,
    });

    // Parse.
    parsing.parseOrder(order);

    // Render.
    await render(order);
};

/**
 * Export command to application.
 */
export default (program) => {
    program
        .command('view-order', 'View open details')
        .argument('<orderId>', 'Order ID', parsing.parseUUID)
        .action((args, opts) => executor(execute, args, opts));
}
