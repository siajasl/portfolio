/**
 * @fileOverview Displays order book information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import { ExchangeTypeEnum, BookSideEnum, OrderTypeEnum } from '../../utils/enums';
import render from '../../renderers/renderNewOrder';

// Target exchange identifier.
const EXCHANGE = ExchangeTypeEnum.TT01;

/**
 * Command execution entry point.
 */
const execute = async (args, { customerReferenceId: customerReferenceID }) => {
    // Push.
    const order = await API.trading.addOrder({
        customerID: cache.getCustomerID(),
        customerReferenceID,
        exchange: EXCHANGE,
        options: {
            ...cache.getTradingPreferences()
        },
        ...cache.getAssetAddresses(args.assetPair),
        ...args
    });

    // Parse.
    parsing.parseNewOrder(order);

    // Render.
    await render(order)
};

/**
 * Export command to application.
 */
export default (program) => {
    program
        .command('add-standard-limit-buy', `Place a standard LIMIT BUY order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .argument('<price>', 'Price at which order is to be placed', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.BUY,
            type: OrderTypeEnum.LIMIT,
            ...args
        }, opts));

    program
        .command('add-standard-limit-sell', `Place a standard LIMIT SELL order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .argument('<price>', 'Price at which order is to be placed', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.SELL,
            type: OrderTypeEnum.LIMIT,
            ...args
        }, opts));

    program
        .command('add-standard-market-buy', `Place a standard MARKET BUY order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.BUY,
            type: OrderTypeEnum.MARKET,
            ...args
        }, opts));

    program
        .command('add-standard-market-sell', `Place a standard MARKET SELL order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.SELL,
            type: OrderTypeEnum.MARKET,
            ...args
        }, opts));
}
