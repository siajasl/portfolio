/**
 * @fileOverview Await settlement channel initiation events.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing, sleep } from '../../utils/index';
import renderEvent from '../../renderers/renderSettlementEvent';
import { renderMessage } from '../../renderers/utils';

// Milliseconds between each loop iteration.
const LOOP_INTERVAL_MS = 100;

/**
 * Command execution entry point.
 */
const execute = async (args, opts) => {
    await setEventHandlers();
    await setEventSubscriptions();
    await renderMessage('awaiting events ...');
    while (true) {
        await sleep(LOOP_INTERVAL_MS);
    }
};

/**
 * Sets handlers that will process events published by API.
 */
const setEventHandlers = async () => {
    API.clearing.events.on('onSettlementInitiate', eventCallback);
    API.clearing.events.on('onSettlementParticipate', eventCallback);
}

/**
 * Sets subscriptions by binding to API endpoints.
 */
const setEventSubscriptions = async () => {
    const customerID = cache.getCustomerID();
    await API.clearing.onSettlementInitiate({ customerID });
    await API.clearing.onSettlementParticipate({ customerID });
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
        .command('settle-async', 'Awaits trade clearing events & executes settlements')
        .action((args, opts) => executor(execute, args, opts));
};
