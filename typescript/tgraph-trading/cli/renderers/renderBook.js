/**
 * @fileOverview Renders order book information.
 */

import 'colors';
import { default as Table } from 'cli-table2';
import { default as wsize } from 'window-size';
import { renderHeader } from './utils';

/**
 * Renders an order book returned from GQL server.
 */
export default async (book) => {
    // Render: header.
    await renderHeader();

    // Set options: main table.
    const windowWidth = wsize.get().width;
    const { mainTableWidth, innerTableWidth } = calculateTableWidths(windowWidth);
    const table = new Table({
        head: ['BIDS'.bold.white, 'ASKS'.bold.white],
        colWidths: [mainTableWidth, mainTableWidth]
    });

    // Set options: inner tables.
    const innerTableOptions = {
        head: ['Price'.bold.white, `Depth (${book.assetPair})`.bold.white],
        colWidths: [innerTableWidth, innerTableWidth]
    };

    // Render: main table.
    table.push([
        getOrderTreeTable(book.bids, innerTableOptions),
        getOrderTreeTable(book.asks, innerTableOptions)
    ]);

    await console.log(table.toString() + '\n');
};

/**
 * Renders one side (i.e. an order tree) of an order book.
 */
const getOrderTreeTable = (orderTree, tblOptions) => {
    const tbl = new Table(tblOptions);
    orderTree.forEach((orderList) => {
        tbl.push([
            String(` ${orderList.price} `).yellow,
            String(` ${orderList.quantity} `).white
        ]);
    });

    return tbl.toString();
}

/**
 * Takes in an window width and returns object with appropriate table widths for the orderbook.
 */
const calculateTableWidths = (windowWidth) => {
    const borderOffset = 4
    const numTables = 2
    const mainTableWidth = Math.round((windowWidth - borderOffset) / numTables)
    const innerTableWidth = Math.round((mainTableWidth - (borderOffset + 1.5)) / numTables)

    return { mainTableWidth, innerTableWidth }
};
