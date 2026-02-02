/**
 * @fileOverview Encapsulates all information related to a DLT transaction.
 */

const uuidv4 = require('uuid/v4');
import { TransactionStateEnum } from './enums';

/**
 * A settlement of an asset exchange between 2 counter-parties.
 * @constructor
 */
export class Transaction {
    constructor ({
        asset,
        hash,
        script,
        secret,
        signature,
        state,
        timestamp,
        transactionID,
        type
    }) {
        // DLT asset.
        this.asset = asset;

        // DLT transaction's unique identifier (i.e. hash).
        this.hash = hash || null;

        // The script to run in the case of UTXO DLT's.
        this.script = script || null;

        // A secret used primarily in redemption scenarios.
        this.secret = secret || null;

        // A signature used in various scenarios.
        this.signature = signature || null;

        // DLT transaction's current state.
        this.state = state || TransactionStateEnum.NEW;

        // Internal timestamp for tracking scenarios.
        this.timestamp = timestamp || new Date();

        // Internal identifier for x-referencing purposes.
        this.transactionID = transactionID || uuidv4();

        // Transaction type, i.e. CONTRACT | REDEEM | REFUND.
        this.type = type || null;
    }
}
