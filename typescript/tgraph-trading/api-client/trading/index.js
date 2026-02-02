/**
 * @fileOverview Sub-package entry point.
 */

import { eventEmitter as events } from './utils/events';
import * as enums from './utils/enums';
import addOrder from './doAddOrder';
import cancelOrder from './doCancelOrder';
import getOrder from './getOrder';
import getOpenOrders from './getOpenOrders';
import getBook from './getBook';
import getTradeHistory from './getTradeHistory';
import onBookUpdate from './onBookUpdate';
import onOrderFill from './onOrderFill';


export {
    // event emitter
    events,

    // enums passed into endpoints
    enums,

    // queries
    getOpenOrders,
    getOrder,
    getBook,
    getTradeHistory,

    // mutations
    addOrder,
    cancelOrder,

    // subscriptions
    onBookUpdate,
    onOrderFill,
};
