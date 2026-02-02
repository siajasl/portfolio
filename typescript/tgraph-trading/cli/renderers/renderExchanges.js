/**
 * @fileOverview Renders settings information.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Renders an order book returned from GQL server.
 */
export default async (exchanges) => {
    await renderHeader()
    await renderTable('Exchanges', exchanges.join('\n'), []);
};
