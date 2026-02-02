/**
 * @fileOverview Library rendering utility functions.
 */

/**
 * Renders a rectangle.
 * @param {CanvasRenderingContext2D} $ctx - 2D canvas renderer.
 * @param {int} x1 - X axis pixel position.
 * @param {int} y1 - y axis pixel position.
 * @param {int} width - Rectangle width.
 * @param {int} height - Rectangle height.
 */
export const renderRect = async ($ctx, x1, y1, width, height) => {
    $ctx.beginPath();
    $ctx.rect(x1, y1, width, height);
    $ctx.fillStyle = 'white';
    $ctx.fill();
};

/**
 * Renders a line.
 * @param {CanvasRenderingContext2D} $ctx - 2D canvas renderer.
 * @param {int} x1 - X axis pixel position.
 * @param {int} y1 - y axis pixel position.
 * @param {int} x2 - X axis pixel position.
 * @param {int} y2 - Y axis pixel position.
 */
export const renderLine = async ($ctx, x1, y1, x2, y2, lineWidth) => {
    $ctx.strokeStyle = 'black';
    $ctx.lineWidth = lineWidth || 2;
    $ctx.beginPath();
    $ctx.moveTo(x1, y1);
    $ctx.lineTo(x2, y2);
    $ctx.stroke();
};

/**
 * Renders a text block.
 * @param {CanvasRenderingContext2D} $ctx - 2D canvas renderer.
 * @param {font} $ctx - 2D canvas renderer.
 * @param {str} text - Text to be rendered.
 * @param {str} font - Font name + size.
 * @param {int} x - X axis pixel position.
 * @param {int} y - y axis pixel position.
 */
export const renderText = async ($ctx, text, font, x, y) => {
    $ctx.save();
    $ctx.fillStyle = 'black';
    $ctx.font = font;
    $ctx.textAlign = 'center';
    $ctx.fillText(text, x, y);
    $ctx.restore();
};
