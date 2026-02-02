/**
 * @fileOverview Resolver pipeline instantiatiator.
 */

import * as _ from 'lodash';
import { createResolver } from 'apollo-resolvers';
import * as base from './base';
import * as logger from '../../../utils/logging';
import authenticate from './authentication';
import authorize from './authorization';
import connectToDb from './mongo';

/**
 * Returns a resolver wrapped in a pipeline.
 */
export default function(leaf, logMessage) {
    return [
        [base, 'resolving gql'],
        [authenticate, 'authenticating'],
        [connectToDb, 'connecting to dB'],
        [authorize, 'authorizing'],
        [leaf, logMessage]
    ]
    .map(([resolver, logMessage], idx) => {
        return [idx, resolver, logMessage];
    })
    .reduce(wrapResolver, null);
}

/**
 * Wraps a resolver to simplify orthogonal behaviours.
 */
const wrapResolver = (parent, [idx, child, logMessage]) => {
    const inner = async (parent, args, context, info) => {
        if (idx === 0) {
            logger.logGQL(`${logMessage} :: ${info.fieldName}`);
        }
        return _.isFunction(child) ? child(parent, args, context, info) : null;
    };
    const outer = parent ? parent.createResolver : createResolver;

    return outer(inner, parent ? parent.resolveError : null);
}
