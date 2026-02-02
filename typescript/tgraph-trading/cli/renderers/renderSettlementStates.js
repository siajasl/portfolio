/**
 * @fileOverview CLI output renderer.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Renders clearing settlement states.
 */
export default async (states) => {
    await renderHeader()
    await renderTable('Settlement States', states.join('\n'), []);
};
