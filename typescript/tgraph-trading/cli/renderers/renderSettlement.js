/**
 * @fileOverview Renders clearing settlements.
 */

import { renderHeader, renderTable } from './utils';
import renderChannel from './renderChannel';

/**
 * Render a table of clearing settlement information.
 */
export default async (settlement) => {
    await renderHeader();
    await renderTable('Settlement Details', settlement.settlementID,
        [
            ['Symbol', settlement.assetPair],
            ['State', settlement.state],
            ['Counter Party One' + (settlement.isCustomerCounterPartyOne ? ' *' : ''), settlement.counterPartyOneID],
            ['Counter Party Two' + (settlement.isCustomerCounterPartyOne ? '' : ' *'), settlement.counterPartyTwoID],
            ['Initiate Channel ID', settlement.initiateChannel.channelID],
            ['Participate Channel ID', settlement.participateChannel.channelID],
            ['Timestamp', settlement.timestamp],
        ]
    );
    await renderChannel(settlement.initiateChannel);
    await renderChannel(settlement.participateChannel);
}
