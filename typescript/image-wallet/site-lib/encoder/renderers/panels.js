/**
 * @fileOverview Header renderer.
 */

import * as DEFAULTS from '../defaults/index';
import { renderLine, renderRect } from '../../utils/rendering';

/**
 * Header renderer.
 * @param {EncodingContextInfo} ctx - Encoding processing context information.
 */
export default async function(ctx) {
    ctx.$ctx.save();
    await renderTop(ctx.$ctx);
    await renderBottom(ctx.$ctx);
    ctx.$ctx.restore();
}

/**
 * Renders top panel.
 * @param {CanvasRenderingContext2D} $ctx - 2D canvas renderer.
 */
const renderTop = async ($ctx) => {
    await renderRect($ctx, 0, 20, DEFAULTS.CANVAS.width, 115);
    await renderLine($ctx, 0, 20, DEFAULTS.CANVAS.width, 20);
    await renderLine($ctx, 0, 135, DEFAULTS.CANVAS.width, 135);
};

/**
 * Renders bottom panel.
 * @param {CanvasRenderingContext2D} $ctx - 2D canvas renderer.
 */
const renderBottom = async ($ctx) => {
    await renderRect(
        $ctx,
        0,
        DEFAULTS.CANVAS.height - 60,
        DEFAULTS.CANVAS.width,
        40,
    );
    await renderLine(
        $ctx,
        0,
        DEFAULTS.CANVAS.height - 60,
        DEFAULTS.CANVAS.width,
        DEFAULTS.CANVAS.height - 60,
    );
    await renderLine(
        $ctx,
        0,
        DEFAULTS.CANVAS.height - 20,
        DEFAULTS.CANVAS.width,
        DEFAULTS.CANVAS.height - 20,
    );
};
