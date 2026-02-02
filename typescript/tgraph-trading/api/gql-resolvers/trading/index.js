/**
 * @fileOverview Trading engine endpoints.
 */

import addOrder from './doAddOrder';
import cancelOrder from './doCancelOrder';
import getDailyVolume from './getDailyVolume';
import getOpenOrders from './getOpenOrders';
import getOrder from './getOrder';
import getBook from './getBook';
import getTradeHistory from './getTradeHistory';
import onBookUpdate from './onBookUpdate';
import onOrderFill from './onOrderFill';

// Mutation set.
export const mutation = {
    addOrder,
    cancelOrder
}

// Query set.
export const query = {
    getDailyVolume,
    getOpenOrders,
    getOrder,
    getBook,
    getTradeHistory,
}

// Subscription set.
export const subscription = {
    onBookUpdate,
    onOrderFill,
}

// Super set.
export const all = {
    addOrder,
    cancelOrder,
    getDailyVolume,
    getOpenOrders,
    getBook,
    getTradeHistory,
    onBookUpdate,
    onOrderFill
}
