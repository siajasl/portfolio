/**
 * @fileOverview Represents information required to settle an asset exchange between counter-parties.
 */

const uuidv4 = require('uuid/v4');
import { SettlementStateEnum } from './enums';

/**
 * A settlement of an asset exchange between 2 counter-parties.
 * @constructor
 * @param {shared.AssetPair} assetPair - Assets being exchanged over channels.
 * @param {CounterParties} counterParties - Settlement participation customers.
 * @param {Channel} initiateChannel - Settlement initiation channel opened by counter party 1.
 * @param {Channel} participateChannel - Settlement participation channel opened by counter party 2.
 * @param {String} settlementID - Internal identifier for x-referencing purposes.
 * @param {String} state - Current settlement state.
 * @param {String} settlementID - Internal identifier for x-referencing purposes.
 */
export class Settlement {
    constructor ({
        assetPair,
        counterParties,
        initiateChannel,
        participateChannel,
        settlementID,
        state,
        timestamp,
    }) {
        // Assets being exchanged over channels.
        this.assetPair = assetPair;

        // Counter parties clearing a set of trades over a pair of channels.
        this.counterParties = counterParties;

        // DLT channel 1: initiate settlement.
        this.initiateChannel = initiateChannel;

        // DLT channel 2: participate settlement.
        this.participateChannel = participateChannel;

        // Internal identifier of settlement.
        this.settlementID = settlementID || uuidv4();

        // State based upon states of channels.
        this.state = state || SettlementStateEnum.New;

        // Unix timestamp for tracking scenarios.
        this.timestamp = timestamp || new Date();
    }

    /**
     * Returns set of channels associated with settlement process.
     */
    get channels() {
        return [
            this.initiateChannel,
            this.participateChannel
        ];
    }
}
