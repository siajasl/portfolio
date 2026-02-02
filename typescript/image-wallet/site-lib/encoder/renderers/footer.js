/**
 * @fileOverview Footer renderer.
 */

import * as DEFAULTS from '../defaults/footer';
import { renderText } from '../../utils/rendering';

/**
 * Footer renderer.
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
    if (!options || !options.footer) {
        return DEFAULTS;
    }
    return {
        text: options.footer.text || DEFAULTS.text,
        font: options.footer.font || DEFAULTS.font,
    }
};
