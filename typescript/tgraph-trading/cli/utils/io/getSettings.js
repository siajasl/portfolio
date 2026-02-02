/**
 * @fileOverview Initialises settings.json file.
 */

import * as fs from 'fs';
import * as settings from './settings';
import { AssetTypeEnum, OrderFillPreferenceEnum } from '../enums';

/**
 * Initialises settings.json file.
 */
export default async () => {
    if (settings.exists() === false) {
        return await settings.write(getNew())
    }
    return await settings.read();
}

/**
 * Returns new settings to be written to file system.
 */
const getNew = () => {
    const data = {
        'accessfile': {
            path: ''
        },
        'assets': {},
        'preferences': {
            fillQuantity: OrderFillPreferenceEnum.PARTIAL,
            fillLowerBound: 0,
        }
    };

    for (const assetType of Object.keys(AssetTypeEnum)) {
        data['assets'][assetType] = {
            address: null
        };
    }

    return data;
}
