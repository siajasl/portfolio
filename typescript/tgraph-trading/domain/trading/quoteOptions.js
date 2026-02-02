/**
 * @fileOverview Various trading options that may or may not affect order processing behaviour.
 */

import { BigNumber } from 'bignumber.js';
import { OrderFillPreferenceEnum } from './enums';

/**
 * Various trading options that may or may not affect order processing behaviour.
 */
export class QuoteOptions {
    constructor ({
        fillPreference,
        fillLowerBound,
        merchantID,
        otcOrderID
    }) {
        // Customer defined fill preference - ALL | PARTIAL.
        this.fillPreference = fillPreference || OrderFillPreferenceEnum.ALL;

        // Lowest quantity fill that customer will accept.
        this.fillLowerBound = new BigNumber(fillLowerBound || '0');

        // Merchant identifier - used on merchant type exchanges only.
        this.merchantID = merchantID;

        // OTC order identifier - used on OTC type exchanges only.
        this.otcOrderID = otcOrderID;
    }
}
