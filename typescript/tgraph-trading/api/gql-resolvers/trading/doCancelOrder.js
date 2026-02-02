/**
 * @fileOverview Cancels an order.
 */

import { CONSTANTS } from '../../utils/imports';
import compose from '../shared/resolvers/factory';
import onBookUpdate from './onBookUpdate';

// Destructure enums.
const { trading : { OrderStateEnum} } = CONSTANTS;

/**
 * Removes an order.
 */
const resolve = async (parent, { input }, context, info) => {
    // Destructure.
    const { customerID, orderID } = input;

    // Remove from exchange.
    await removeFromExchange(context, customerID, orderID);

    // Update database.
    await updateDb(context, customerID, orderID);

    return true;
};

/**
 * Removes an order from a trading exchange.
 */
const removeFromExchange = async ({ exchanges, pubsub }, customerID, orderID) => {
    for (const exchange of exchanges) {
        // Get order.
        const order = exchange.getOrder(orderID);
        if (!order || order.customerID !== customerID) {
            continue
        }

        // Remove from exchange.
        const { book } = exchange.removeOrder(order);

        // Publish event: onBookUpdate.
        await onBookUpdate.publish(pubsub, { book });
    }
}

/**
 * Updates order information in database.
 */
const updateDb = async ({ dbe }, customerID, orderID) => {
    const order = await dbe.getOrder({ customerID, orderID });
    if (order) {
        await dbe.updateOrder({ orderID }, {
            $set: {
                state: OrderStateEnum.CANCELLED,
            }
        });
    } else {
        throw new Error('Customer order not found');
    }
}

// Export composed resolver.
export default compose(resolve, 'remove order');
