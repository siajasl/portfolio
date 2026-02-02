/**
 * @fileOverview Wraps imports of various libs so as to ensure that they
 *               are importable in node, test & browser environments.
 */

// Export access file.
import * as AF from '@trinkler/accessfile';

// Export access file core.
import * as AFC from '@trinkler/accessfile-core';

// Export API client for internal use.
import * as API from '@trinkler/trade-api-client';

// Export domain constants.
import * as CONSTANTS from '@trinkler/trade-constants';

// Export swaps.
import * as SWAPS from '@trinkler/trade-swap';

export {
    AF,
    AFC,
    API,
    CONSTANTS,
    SWAPS
};
