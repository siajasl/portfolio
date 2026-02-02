/**
 * @fileOverview Renders a customer's list of orders.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Rendering entry point.
 */
export default async (orders) => {
    await renderHeader();

    if (orders.length === 0) {
        await renderTable('Order Details', 'No orders were found', []);
        return;
    }

    for (const o of orders) {
        const idx = `(#${orders.indexOf(o) + 1} of ${orders.length})`;
        await renderTable(`Order Details ${idx}`, o.orderID,
            [
                ['Exchange', o.exchange],
                ['Symbol', o.assetPair],
                ['Type', o.type],
                ['Side', o.side],
                ['Price', o.price],
                ['Quantity', o.quantity],
                ['Quantity - Filled', o.quantityFilled],
                ['Status', o.state],
                ['Customer Order ID', o.customerReferenceID || 'N/A'],
                [`Settlement ${o.assetPair.slice(0, 3)} Address`, o.addressOfBaseAsset],
                [`Settlement ${o.assetPair.slice(3)} Address`, o.addressOfQuoteAsset],
                ['Timestamp', o.timestamp],

            ]
        );
    }
};
