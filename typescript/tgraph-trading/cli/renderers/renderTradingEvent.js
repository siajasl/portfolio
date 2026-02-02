/**
 * @fileOverview Renders trading related event infomration.
 */

import { renderHeader, renderTable } from './utils';
import renderChannel from './renderChannel';

/**
 * Render a table of trading event information.
 */
export default async (eventData) => {
    await renderTable(
        'Trading Event',
        eventData.eventType.slice(2),
        getFieldset(eventData)
    );
}

/**
 * Returns field set for rendering.
 */
const getFieldset = (eventData) => {
    const fields = []
    fields.push(['Event Timestamp', eventData.timestamp]);

    return fields;
}
