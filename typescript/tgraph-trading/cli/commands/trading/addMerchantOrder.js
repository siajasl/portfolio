/**
 * @fileOverview Merchant make order.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import { ExchangeTypeEnum, BookSideEnum, OrderTypeEnum } from '../../utils/enums';
import render from '../../renderers/renderNewOrder';

// Target exchange identifier.
const EXCHANGE = ExchangeTypeEnum.TT03;

/**
 * Command execution entry point.
 */
const execute = async (args, { customerReferenceId: customerReferenceID, merchantId: merchantID }) => {
    // Push.
    const order = await API.trading.addOrder({
        customerID: cache.getCustomerID(),
        customerReferenceID,
        exchange: EXCHANGE,
        options: {
            merchantID,
            ...cache.getTradingPreferences()
        },
        ...args,
        ...cache.getAssetAddresses(args.assetPair),
    });

    // Parse.
    parsing.parseOrder(order);

    // Render.
    await render(order);
};

/**
 * Export command to application.
 */
export default (program) => {
    program
        .command('add-merchant-limit-buy', `Place a merchant LIMIT BUY order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .argument('<price>', 'Price at which order is to be placed', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .option('--merchantId <merchantId>', 'Merchant identifier for settlement purposes')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.BUY,
            type: OrderTypeEnum.LIMIT,
            ...args
        }, opts));

    program
        .command('add-merchant-limit-sell', `Place a merchant LIMIT SELL order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .argument('<price>', 'Price at which order is to be placed', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .option('--merchantId <merchantId>', 'Merchant identifier for settlement purposes')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.SELL,
            type: OrderTypeEnum.LIMIT,
            ...args
        }, opts));

    program
        .command('add-merchant-market-buy', `Place a merchant MARKET BUY order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .option('--merchantId <merchantId>', 'Merchant identifier for settlement purposes')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.BUY,
            type: OrderTypeEnum.MARKET,
            ...args
        }, opts));

    program
        .command('add-merchant-market-sell', `Place a merchant MARKET SELL order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .option('--merchantId <merchantId>', 'Merchant identifier for settlement purposes')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.SELL,
            type: OrderTypeEnum.MARKET,
            ...args
        }, opts));
}
