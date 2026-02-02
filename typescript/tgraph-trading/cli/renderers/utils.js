import 'colors';
import { default as Table } from 'cli-table2';
import { default as wsize } from 'window-size';

const {
   version: CLI_VERSION
} = require('../../package.json');

/**
 * Renders standard header information.
 */
export const renderHeader = async () => {
    const windowWidth = wsize.get().width;
    const rightHeader = 'https://tradetelegraph.com';
    const leftHeader = `Trade Telegraph CLI - v${CLI_VERSION}`;
    const ui = [' '];
    ui.push(' ' + leftHeader.bold.white
                + Array(windowWidth - 1 - leftHeader.length - rightHeader.length).join(' ')
                + rightHeader.bold.cyan);
    ui.push(' ');

    // console.clear();
    console.log(ui.join('\n'));
};

/**
 * Renders an error for the user.
 */
export const renderError = async ({ message }) => {
    if (message.startsWith('GraphQL error: ')) {
        message = message.slice(15);
    }
    else if (message.startsWith('ERROR: ')) {
        message = message.slice(7);
    }
    await renderHeader();
    await renderTable('ERROR', message, [])
};

/**
 * Renders a message for the user.
 */
export const renderMessage = async (msg, doRenderHeader) => {
    if (doRenderHeader !== false) {
        await renderHeader();
    }
    await renderTable(msg, '', [])
};

/**
 * Render a table.
 */
export const renderTable = async (caption, subCaption, fields) => {
    const windowWidth = wsize.get().width;
    const table = new Table({
        head: subCaption && subCaption.length > 0 ? [caption, subCaption] :
                                                    [{ content:caption, colSpan:2 }],
        style: { head: ['white'] },
        colWidths: [
            Math.round((windowWidth - 4) / 4),
            3 * Math.round((windowWidth - 4) / 4)
        ]
    });
    for (const [caption, value] of fields) {
        table.push([caption, value]);
    }

    console.log(table.toString() + '\n');
}
