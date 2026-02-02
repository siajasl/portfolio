/**
 * @fileOverview Renders clearing settlements.
 */

import { renderHeader, renderTable } from './utils';
import * as cache from '../utils/cache';

/**
 * Rendering entry point.
 */
export default async (assetPair, trades) => {
    await renderHeader();

    if (trades.length === 0) {
        await renderTable('Trade History Details', 'No trades were found', []);
        return;
    }

    for (const t of trades) {
        renderTable('Trade Details', t.tradeID,
            [
                ['Symbol', t.assetPair],
                ['Side', t.side],
                ['Price', t.price],
                ['Amount', t.quantity],
                ['Status', t.state],
                ['Order ID', t.orderID],
                ['Settlement ID', t.settlementID],
                ['Timestamp', t.timestamp],
            ]
        );
    }
};
