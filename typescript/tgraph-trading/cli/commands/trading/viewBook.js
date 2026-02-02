/**
 * @fileOverview Pulls & renders order book information.
 */

import { executor, parsing } from '../../utils/index';
import { API } from '../../utils/imports';
import { ExchangeTypeEnum } from '../../utils/enums';
import render from '../../renderers/renderBook';

/**
 * Command execution entry point.
 */
const execute = async (args, { depth }) => {
    // Pull.
    const book = await API.trading.getBook({
        askDepth: depth,
        bidDepth: depth,
        ...args
    });

    // Parse.
    parsing.parseBook(book);

    // Render.
    await render(book);
};

/**
 * Export command to application.
 */
export default (program) => {
    const config = [
        [ExchangeTypeEnum.TT01, 'standard'],
        [ExchangeTypeEnum.TT02, 'otc'],
        [ExchangeTypeEnum.TT03, 'merchant'],
    ];

    for (const [exchangeType, bookType] of config) {
        program
            .command(
                `view-${bookType}-book`,
                `View ${bookType} order book information`
            )
            .argument('<assetPair>', 'Market of asset pair', parsing.parseAssetPair)
            .option('--depth <integer>', 'Number of asks & bids', program.INT, 25)
            .action((args, opts) => executor(execute, {
                exchange: exchangeType,
                ...args
            }, opts));
    }
}
