/**
 * @fileOverview Encoding entry point.
 */

import { logInfo, logTODO, logWarning } from '../utils/logging';
import renderCanvas from './renderers/canvas';
import renderFooter from './renderers/footer';
import renderHeader from './renderers/header';
import renderIdenticon from './renderers/identicon';
import renderPanels from './renderers/panels';
import renderQrCode from './renderers/qrCode';
import renderWarning from './renderers/warning';
import setCipherText from './setCipherText';
import validateOutput from './validateOutput';

// Maximum attempts before rendering is aborted.
const MAX_ATTEMPTS = 5;

/**
 * Encodes an access file from user credentials and encoding options.
 * @param {object} credentials - Credentials to be encoded.
 * @param {number} purposeId - A number identifying the purpose of this wallet.
 * @param {object} options - Encoding options.
 * @return An access file encoded as an HTMLCanvasElement.
 */
export default async function(credentials, purposeId, options) {
    // Defensive programming.
    validateInputs(credentials, options);

    // Invoke rendering pipeline.
    // N.B. for some non-deterministic reason the rendering pipeline fails
    //      approx 30% of the time - hence the retries.
    let ctx = new EncodingContextInfo(credentials, purposeId, options);
    let attempts = 0;
    do {
        // Increment attempt count.
        attempts += 1;
        logInfo(`Canvas rendering attempt #${attempts}: begin`);

        // Invoke pipeline.
        await setCipherText(ctx);
        await renderCanvas(ctx);
        await renderPanels(ctx);
        await renderFooter(ctx);
        await renderHeader(ctx);
        await renderWarning(ctx);
        await renderIdenticon(ctx);
        await renderQrCode(ctx);

        // Verify rendering.
        let wasRendered = await validateOutput(ctx);

        // Either return or loop.
        if (wasRendered) {
            logInfo(`Canvas rendering attempt #${attempts}: success`);
            return { $canvas: ctx.$canvas };
        } else {
            logWarning(`Canvas rendering attempt #${attempts}: failed`);
            ctx = new EncodingContextInfo(credentials, options);
        }
    } while (attempts < MAX_ATTEMPTS);

    // If not rendered then throw error.
    throw new Error('Access file rendering failed');
}

/**
 * Validates encoding inputs.
 * @param {EncodingContextInfo} ctx - Encoding processing context information.
 */
const validateInputs = (credentials, options) => {
    logTODO(`validate password [${credentials.password}] minimum length = 8 ?`);
};

/**
 * Contextual information passed along encoding pipeline.
 * @constructor
 * @param {object} credentials - Credentials to be encoded.
 * @param {object} options - Encoding options.
 */
class EncodingContextInfo {
    constructor(credentials, purposeId, options) {
        this.cipherText = null;
        this.credentials = credentials;
        this.purposeId = purposeId;
        this.options = options;
        this.$canvas = null;
        this.$ctx = null;
    }
}
