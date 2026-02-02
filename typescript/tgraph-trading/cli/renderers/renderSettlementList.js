/**
 * @fileOverview Renders clearing settlements.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Rendering entry point.
 */
export default async (settlements) => {
    await renderHeader();

    if (settlements.length === 0) {
        await renderTable('Settlement Details', 'No settlements were found', []);
        return;
    }

    for (const s of settlements) {
        const idx = `(#${settlements.indexOf(s) + 1} of ${settlements.length})`;
        await renderTable(`Settlement Details ${idx}`, s.settlementID,
            [
                ['Symbol', s.assetPair],
                ['State', s.state],
                ['Counter Party One' + (s.isCustomerCounterPartyOne ? ' *' : ''), s.counterPartyOneID],
                ['Counter Party Two' + (s.isCustomerCounterPartyOne ? '' : ' *'), s.counterPartyTwoID],
                ['Initiate Channel ID', s.initiateChannel.channelID],
                ['Participate Channel ID', s.participateChannel.channelID],
                ['Timestamp', s.timestamp],
            ]
        );
    }
};
