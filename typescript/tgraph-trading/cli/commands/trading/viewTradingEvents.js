/**
 * @fileOverview Awaits trading events & displays relevant information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing, sleep } from '../../utils/index';
import { renderMessage } from '../../renderers/utils';
import renderEvent from '../../renderers/renderTradingEvent';

// Milliseconds between each loop iteration.
const LOOP_INTERVAL_MS = 100;

/**
 * Command execution entry point.
 */
const execute = async (args, opts) => {
    await setEventHandlers();
    await setEventSubscriptions();
    await renderMessage('awaiting trading events ...');
    while (true) {
        await sleep(LOOP_INTERVAL_MS);
    }
};

/**
 * Sets handlers that will process events published by API.
 */
const setEventHandlers = async () => {
    API.trading.events.on('onOrderFill', eventCallback);
    API.trading.events.on('onBookUpdate', eventCallback);
}

/**
 * Sets subscriptions by binding to API endpoints.
 */
const setEventSubscriptions = async () => {
    await API.trading.onBookUpdate();
    await API.trading.onOrderFill({
        customerID: cache.getCustomerID()
    });
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
        .command('view-trading-events', 'Displays trading event stream')
        .action((args, opts) => executor(execute, args, opts));
};
