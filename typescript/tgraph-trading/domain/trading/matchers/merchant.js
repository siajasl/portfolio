/**
 * @fileOverview A standard matching algorithm: i.e. accepts limit/market orders.
 */

import { isNil } from 'lodash';
import * as standard from './standard';

/*
 * Submits limit/market orders to a merchant book.
 * @param {Book} book - A merchant order book supported by an exchange.
 * @param {Order} order - An order being sumitted to an exchange.
 */
export const submit = (book, order) => {
    return standard.submit(book, order, canMatch);
}

/*
 * Predicate returning flag indicating whether 2 orders can be matched.
 * @param {Order} o1 - An order being submitted to an exchange, i.e. a take.
 * @param {Order} o2 - An order submitted to an exchange, i.e. a make.
 */
const canMatch = (o1, o2) => {
    // False if standard tests fail.
    if (standard.canMatch(o1, o2) === false) {
        return false;
    }

    // False if both orders specify a merchant id.
    if (o1.options.merchantID && o2.options.merchantID) {
        return false;
    }

    // False if neither order specifies a merchant id.
    if (isNil(o1.options.merchantID) && isNil(o2.options.merchantID)) {
        return false;
    }

    return true;
};
