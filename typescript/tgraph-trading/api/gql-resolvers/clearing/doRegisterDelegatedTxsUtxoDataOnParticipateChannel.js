/**
 * @fileOverview Registers participateChannel delegated UTXO transaction data.
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
            "participateChannel.txRedeem.stateHistory": {
                state: TransactionStateEnum.SCRIPT
            },
            "participateChannel.txRefund.stateHistory": {
                state: TransactionStateEnum.SIGNED
            }
        },
        $set: {
          "participateChannel.txRedeem.script": redeemScript,
          "participateChannel.txRedeem.state": TransactionStateEnum.SCRIPT,
          "participateChannel.txRefund.script": refundTransaction,
          "participateChannel.txRefund.state": TransactionStateEnum.SIGNED,
          "participateChannel.redeemFee": redeemFee,
          "participateChannel.refundFee": refundFee,
        },
    });

    return true;
};

// Export composed resolver.
export default compose(resolve, 'register participateChannel delegated UTXO transaction data');
