/**
 * @fileOverview Registers initiateChannel delegated UTXO transaction data.
 */

import { CONSTANTS } from '../../utils/imports';
import compose from '../shared/resolvers/factory';

// Destructure enums.
const { clearing : { TransactionStateEnum } } = CONSTANTS;

/**
 * Endpoint resolver.
 */
const resolve = async (parent, { input }, { dbe }, info) => {
    // Destructure.
    const {
        redeemScript,
        refundTransaction,
        settlementID,
        redeemFee,
        refundFee,
    } = input;

    // Persist.
    await dbe.updateSettlement({ settlementID }, {
        $push: {
            "initiateChannel.txRedeem.stateHistory": {
                state: TransactionStateEnum.SCRIPT
            },
            "initiateChannel.txRefund.stateHistory": {
                state: TransactionStateEnum.SIGNED
            }
        },
        $set: {
          "initiateChannel.txRedeem.script": redeemScript,
          "initiateChannel.txRedeem.state": TransactionStateEnum.SCRIPT,
          "initiateChannel.txRefund.script": refundTransaction,
          "initiateChannel.txRefund.state": TransactionStateEnum.SIGNED,
          "initiateChannel.redeemFee": redeemFee,
          "initiateChannel.refundFee": refundFee,
        },
    });

    return true;
};

// Export composed resolver.
export default compose(resolve, 'register initiateChannel delegated UTXO transaction data');
