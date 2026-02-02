/**
 * @fileOverview Header renderer.
 */

import * as DEFAULTS from '../defaults/header';
import { renderText } from '../../utils/rendering';

/**
 * Header renderer.
 * @param {EncodingContextInfo} ctx - Encoding processing context information.
 */
export default async function(ctx) {
    const settings = getSettings(ctx.options);
    renderText(
        ctx.$ctx,
        settings.text,
        settings.font,
        DEFAULTS.x,
        DEFAULTS.y,
    );
}

/**
 * Validates supplied password.
 * @param {string} pwd - Password being validated.
 */
const getSettings = (options) => {
    if (!options || !options.header) {
        return DEFAULTS;
    }
    return {
        text: options.header.text || DEFAULTS.text,
        font: options.header.font || DEFAULTS.font,
    }
};
