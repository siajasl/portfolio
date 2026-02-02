/**
 * @fileOverview A counter-party participating within a settlement.
 */

/**
 * A counter-party participating within a settlement.
 * @constructor
 */
export class CounterParty {
    constructor ({
        customerID,
    }) {
        // Customer identifier.
        this.customerID = customerID;
    }
}
