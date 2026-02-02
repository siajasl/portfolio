/**
 * @fileOverview Canvas renderer.
 */

import * as DEFAULTS from '../defaults/canvas';

/**
 * Canvas renderer.
 * @param {EncodingContextInfo} ctx - Encoding processing context information.
 */
export default async function(ctx) {
    const $canvas = document.createElement('canvas');
    $canvas.classList.add('ts-af');
    $canvas.height = DEFAULTS.height;
    $canvas.width = DEFAULTS.width;

    ctx.$canvas = $canvas;
    ctx.$ctx = $canvas.getContext('2d');
}
