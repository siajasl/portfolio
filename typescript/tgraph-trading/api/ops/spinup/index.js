/**
 * @fileOverview Executes API spinup procedures.
 */

import getExchanges from './getExchanges';
import setBooks from './setBooks';
import setDatabase from './setDatabase';

/**
 * Returns set of supported exchanges (hydrated with existing orders).
 */
export default async (dbe) => {
    // Ensure db is setup correctly.
    await setDatabase(dbe);

    // Pull set of supported exchanges.
    const exchanges = await getExchanges(dbe);

    // Hydrate order books.
    await setBooks(dbe, exchanges);

    return exchanges;
};
