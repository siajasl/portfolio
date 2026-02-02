/**
 * @fileOverview Renders clearing settlements.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Render a table of order information.
 */
export default async (o) => {
    await renderHeader();

    await renderTable('Order Details', o.orderID,
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

    for (const trade of o.trades || []) {
        const idx = `(#${o.trades.indexOf(trade) + 1} of ${o.trades.length})`;
        renderTable(`Trade Details ${idx}`, trade.tradeID, [
            ['Price', trade.price],
            ['Quantity', trade.quantity],
            ['Settlement ID', trade.settlementID],
            ['Status', trade.state],
            ['Timestamp', trade.timestamp],
        ]);
    }
}
