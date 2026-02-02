/**
 * @fileOverview Awaits clearing events & displays relevant information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing, sleep } from '../../utils/index';
import { renderMessage } from '../../renderers/utils';
import renderEvent from '../../renderers/renderSettlementEvent';

// Milliseconds between each loop iteration.
const LOOP_INTERVAL_MS = 100;

/**
 * Command execution entry point.
 */
const execute = async (args, opts) => {
    await setEventHandlers();
    await setEventSubscriptions();
    await renderMessage('awaiting settlement events ...');
    while (true) {
        await sleep(LOOP_INTERVAL_MS);
    }
};

/**
 * Sets handlers that will process events published by API.
 */
const setEventHandlers = async () => {
    API.clearing.events.on('onSettlementComplete', eventCallback);
    API.clearing.events.on('onSettlementFinalise', eventCallback);
    API.clearing.events.on('onSettlementInitiate', eventCallback);
    API.clearing.events.on('onSettlementParticipate', eventCallback);
    API.clearing.events.on('onSettlementStateChange', eventCallback);
}

/**
 * Sets subscriptions by binding to API endpoints.
 */
const setEventSubscriptions = async () => {
    const customerID = cache.getCustomerID();
    await API.clearing.onSettlementComplete({ customerID });
    await API.clearing.onSettlementFinalise({ customerID });
    await API.clearing.onSettlementInitiate({ customerID });
    await API.clearing.onSettlementParticipate({ customerID });
    await API.clearing.onSettlementStateChange({ customerID });
}

/**
 * Event handler.
 */
const eventCallback = async (data) => {
    parsing.parseEventData(data);
    await renderEvent(data);
}

// Export command.
export default (program) => {
    program
        .command('view-settlement-events', 'Displays settlement event stream')
        .action((args, opts) => executor(execute, args, opts));
};
