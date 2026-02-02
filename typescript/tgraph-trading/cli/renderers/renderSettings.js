/**
 * @fileOverview Renders settings information.
 */

import { renderHeader, renderTable } from './utils';

/**
 * Renders config settings.
 */
export default async (assetAddresses, tradingPreferences, accessFile) => {
    await renderHeader()
    await renderTable('Access File', '', [
        ['File path', accessFile.path],
        ['Public Key', accessFile.keyPair ? accessFile.keyPair.publicKey : 'N/A'],
    ]);
    await renderTable('Asset Addresses', '',
        Object.keys(assetAddresses).map((i) => [i, assetAddresses[i]])
    );
    await renderTable('Trading Preferences', '',
        Object.keys(tradingPreferences).map((i) => [i, tradingPreferences[i]])
    );
};
