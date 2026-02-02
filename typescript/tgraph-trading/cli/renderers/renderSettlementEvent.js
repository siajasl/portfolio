/**
 * @fileOverview Renders clearing settlement events.
 */

import { renderHeader, renderTable } from './utils';
import renderChannel from './renderChannel';

/**
 * Render a table of clearing event information.
 */
export default async (eventData) => {
    await renderTable(
        'Settlement Event',
        eventData.eventType.slice(2),
        getFieldset(eventData)
    );
}

/**
 * Returns field set for rendering.
 */
const getFieldset = (eventData) => {
    const fields = [
        ['Settlement ID', eventData.settlementID]
    ]
    if (eventData.eventType === 'onStateChange') {
        fields.push(['Settlement State', eventData.state]);
    }
    fields.push(['Event Timestamp', eventData.timestamp]);

    return fields;
}
