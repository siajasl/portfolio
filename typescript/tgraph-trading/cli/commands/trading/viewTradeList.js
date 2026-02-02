/**
 * @fileOverview Pulls & renders trade list information.
 */

import { cache, executor, parsing } from '../../utils/index';
import { API } from '../../utils/imports';
import render from '../../renderers/renderTradeList';

/**
 * Command execution entry point.
 */
const execute = async (args, { dateFrom, dateTo, state, symbol }) => {
    // Set payload.
    const payload = {
        customerID: cache.getCustomerID(),
        dateFrom: Date.parse(dateFrom) || Date.now() - 31536000000,
        dateTo: Date.parse(dateTo) || Date.now()
    };
    if (symbol) {
        payload['assetPair'] = symbol;
    }
    if (state) {
        payload['state'] = state;
    }

    // Pull.
    const trades = await API.trading.getTradeHistory(payload);

    // Parse.
    trades.forEach(parsing.parseTrade);

    // Render.
    await render(symbol, trades);
};

/**
 * Export command to application.
 */
export default (program) => {
    program
        .command('view-trades', 'View your trade history')
        .option('--symbol <symbol>', 'Market of asset pair', parsing.parseAssetPair)
        .option('--state <state>', 'Current trade status', parsing.parseTradeState)
        .option('--date-from <dateFrom>', 'Date from which trade history will be filtered')
        .option('--date-to <dateTo>', 'Date to which trade history will be filtered')
        .action((args, opts) => executor(execute, args, opts));
}
