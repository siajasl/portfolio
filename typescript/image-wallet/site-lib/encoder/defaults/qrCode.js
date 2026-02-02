/**
 * @fileOverview Wallet header text defaults.
 */

import * as CANVAS from './canvas';

// Default qr code padding.
export const errorCorrectionLevel = 'H';

// Default qr code frame width.
export const frameWidth = 2;

// Default qr code padding.
export const padding = CANVAS.padding;

// Default qr code size.
export const size = 150;

// Default qr code x position.
export const x = (CANVAS.width - size) / 2;

// Default qr code y position.
export const y = (CANVAS.height - size) / 2 + 50;
