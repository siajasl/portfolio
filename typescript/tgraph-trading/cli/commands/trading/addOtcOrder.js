/**
 * @fileOverview Merchant make order.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import { ExchangeTypeEnum, BookSideEnum, OrderTypeEnum } from '../../utils/enums';
import render from '../../renderers/renderNewOrder';

// Target exchange identifier.
const EXCHANGE = ExchangeTypeEnum.TT02;

// Target order type.
const ORDER_TYPE = OrderTypeEnum.LIMIT;

/**
 * Command execution entry point.
 */
const execute = async (args, { customerReferenceId: customerReferenceID, makeOrderId: otcOrderID }) => {
    // Push.
    const order = await API.trading.addOrder({
        customerID: cache.getCustomerID(),
        customerReferenceID,
        exchange: EXCHANGE,
        options: {
            otcOrderID,
            ...cache.getTradingPreferences()
        },
        type: ORDER_TYPE,
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
        .command('add-otc-limit-buy', `Place an OTC LIMIT BUY order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .argument('<price>', 'Price at which order is to be placed', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .option('--makeOrderId <makeOrderId>', 'The identifier of an order for matching purposes')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.BUY,
            type: OrderTypeEnum.LIMIT,
            ...args
        }, opts));

    program
        .command('add-otc-limit-sell', `Place an OTC LIMIT SELL order`)
        .argument('<assetPair>', 'Pair of assets to be traded', parsing.parseAssetPair)
        .argument('<quantity>', 'Amount to be traded', program.FLOAT)
        .argument('<price>', 'Price at which order is to be placed', program.FLOAT)
        .option('--customerReferenceId <customerReferenceId>', 'Customer\'s order identifier for internal cross referencing')
        .option('--makeOrderId <makeOrderId>', 'The identifier of an order for matching purposes')
        .action((args, opts) => executor(execute, {
            side: BookSideEnum.BUY,
            type: OrderTypeEnum.LIMIT,
            ...args
        }, opts));
}
