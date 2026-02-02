/**
 * @fileOverview A set of counter-parties within a clearing process.
 */

import { CounterParty } from './counterParty';

/**
 * A set of counter-parties within a clearing process.
 * @constructor
 */
export class CounterParties {
    constructor (partyOne, partyTwo) {
        // Counter party 1.
        this.partyOne = partyOne;

        // Counter party 2.
        this.partyTwo = partyTwo;
    }

    /**
     * Factory method: instantiates & returns an instance hydrated from a trade.
     * @param {trading.Trade} trade - A matched trade between counter-parties.
     */
    static createFromTrade(trade) {
        const { makeOrder, takeOrder } = trade;

        return new CounterParties(
            new CounterParty(makeOrder),
            new CounterParty(takeOrder),
        )
    }
}
