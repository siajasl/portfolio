/**
 * @fileOverview Renders a clearing channel.
 */

import { renderHeader, renderTable } from './utils';
import renderTransaction from './renderTransaction';

/**
 * Render a table of clearing channel information.
 */
export default async (channel, doRenderHeader, doRenderTransactions) => {
    if (doRenderHeader) {
        await renderHeader();
    }
    
    await renderTable(
        `${channel.type} Channel Details`, channel.channelID,
        [
            ['Symbol', channel.asset],
            ['Amount', channel.amount],
            ['Commission', channel.commission],
            ['Address From', channel.addressFrom],
            ['Address To', channel.addressTo],
            ['Timeout', channel.timeout],
            ['Timestamp', channel.timestamp],
        ]
    );

    if (doRenderTransactions) {
        await renderTransaction(channel, channel.txContract);
        await renderTransaction(channel, channel.txRedeem);
        await renderTransaction(channel, channel.txRefund);
    }
}
