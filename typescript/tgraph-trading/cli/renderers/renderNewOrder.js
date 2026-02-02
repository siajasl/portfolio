/**
 * @fileOverview Renders order book information.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Renders a new order.
 */
export default async (o) => {
    await renderHeader();

    renderTable('New Order Details', o.orderID, [
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
        ['Timestamp', o.timestamp]
    ]);

    for (const t of o.trades) {
        const idx = `(#${o.trades.indexOf(t) + 1} of ${o.trades.length})`;
        renderTable(`Trade Details ${idx}`, t.tradeID, [
            ['Price', t.price],
            ['Quantity', t.quantity],
            ['Status', t.state],
            ['Timestamp', t.timestamp],
        ]);
    }
};
