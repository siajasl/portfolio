/**
 * @fileOverview Renders a clearing transaction.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Render a table of clearing transaction information.
 */
export default async (channel, transaction, doRenderHeader) => {
    if (doRenderHeader) {
        await renderHeader();
    }
    await renderTable(
        `${channel.type} Channel: ${transaction.type} Transaction Details`, '',
        [
            ['State', transaction.state],
            ['Hash', transaction.hash || 'N/A'],
            ['Script', transaction.script || 'N/A'],
            ['Secret', transaction.secret || 'N/A'],
            ['Signature', transaction.signature || 'N/A']
        ]
    );
}
