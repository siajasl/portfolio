/**
 * @fileOverview Renders settings information.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Renders an order book returned from GQL server.
 */
export default async (assetPairs) => {
    await renderHeader()
    await renderTable('Asset Pairs', assetPairs.join('\n'), []);
};
