/**
 * @fileOverview A channel over which an asset transfer will occur.
 */

const uuidv4 = require('uuid/v4');
import { Transaction } from './transaction';
import { TransactionTypeEnum } from './enums';

/**
 * A channel over which an asset transfer will occur.
 * @constructor
 * @param {String} addressFrom - The DLT address from which the asset amount will be transferred.
 * @param {String} addressTo - The DLT address to which the asset amount will be transferred, i.e. the beneficiary.
 * @param {BigNumber} amount - Amount of asset being transferred.
 * @param {String} asset - Type of asset being exchanged over channel.
 * @param {BigNumber} commission - Recommended amount of commission to be paid.
 * @param {String} hashedSecret - In case of PARTICIPATE htlcType send htlcHashedSecret of INITIATE htlc.
 * @param {Integer} timeout - Point in unix time when HTLC is considered to have timed out.
 * @param {Enum} type - Channel type, i.e. INITIATE | PARTICIPATE.
 */
export class Channel {
    constructor ({
        addressFrom,
        addressTo,
        amount,
        asset,
        channelID,
        commission,
        counterParties,
        hashedSecret,
        timeout,
        timestamp,
        txContract,
        txRefund,
        txRedeem,
        type
    }) {
        // The DLT address from which the asset amount will be transferred, i.e the creator.
        this.addressFrom = addressFrom || null;

        // The DLT address to which the asset amount will be transferred, i.e. the beneficiary.
        this.addressTo = addressTo || null;

        // Amount of asset to be exchanged over channel.
        this.amount = amount || null;

        // Type of asset being exchanged over channel.
        this.asset = asset;

        // Internal identifier of channel.
        this.channelID = channelID || uuidv4();

        // Recommended amount of commission to be paid.
        this.commission = commission || null;

        // Counter parties clearing a set of trades over a pair of channels.
        this.counterParties = counterParties;

        // In case of PARTICIPATE htlcType send htlcHashedSecret of INITIATE htlc.
        this.hashedSecret = hashedSecret || null;

        // Quantity of asset to be exchanged over channel.
        this.quantity = amount || null;

        // Point in unix time when HTLC is considered to have timed out.
        this.timeout = timeout || null;

        // Timestamp for tracking scenarios.
        this.timestamp = timestamp || new Date();

        // HTLC deployment transaction information.
        this.txContract = txContract || new Transaction({ asset, type: TransactionTypeEnum.CONTRACT });

        // Redeem transaction information.
        this.txRedeem = txRedeem || new Transaction({ asset, type: TransactionTypeEnum.REDEEM });

        // Refund transaction information.
        this.txRefund = txRefund || new Transaction({ asset, type: TransactionTypeEnum.REFUND });

        // Channel type, i.e. INITIATE | PARTICIPATE.
        this.type = type || null;
    }

    /**
     * Returns set of DLT transactions associated with channel.
     */
    get transactions() {
        return [
            this.txContract,
            this.txRedeem,
            this.txRefund,
        ];
    }
}
