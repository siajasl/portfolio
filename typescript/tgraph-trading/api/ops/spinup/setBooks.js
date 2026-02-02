/**
 * @fileOverview Initialises exchange order books.
 */

import * as logger from '../../utils/logging';
import mapper from '../../mappers/trading/mongoToDomain_order';

/**
 * Returns meta-data regarding setof supported exchanges.
 */
export default async (dbe, exchanges) => {
    // Resubmit all open orders.
    for (const orderDocument of await dbe.getOpenOrders({ customerID: null })) {
        // Set exchange/asset pair.
        const exchange = exchanges.find(i => i.exchangeID === orderDocument.exchangeID);
        const assetPair = exchange.getAssetPair(orderDocument.assetPair);

        // Resubmit order.
        const order = mapper(orderDocument, assetPair);
        exchange.resubmitOrder(order);
    }
};
