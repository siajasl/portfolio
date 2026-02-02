/**
 * @fileOverview GQL endpoint resolver map.
 */

import * as kyc from './kyc/index';
import * as clearing from './clearing/index';
import * as scalars from './shared/scalars/index';
import * as trading from './trading/index';

// Export resolver map.
export default {
    ...scalars,

    Mutation: Object.assign(
        clearing.mutation,
        kyc.mutation,
        trading.mutation
    ),

    Query: Object.assign(
        clearing.query,
        kyc.query,
        trading.query
    ),

    Subscription: Object.assign(
        clearing.subscription,
        trading.subscription
    ),
};
