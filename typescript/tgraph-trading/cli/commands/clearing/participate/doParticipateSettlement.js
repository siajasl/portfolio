/**
 * @fileOverview Participates within trade settlement process.
 */

import { AuditContractArgsFactory, ParticipateArgsFactory } from '.';
import { HtlcFactory } from '../utils/htlcFactory';
import { cache, executor, parsing } from '../../../utils';
import { API, AFC } from '../../../utils/imports';
import { auditValidators } from './validateAudit';
import { capitalizeFirstLetter } from '../../../utils/capitalizeFirstLetter';

/**
 * Establishes swap participation channel.
 */
export default async (settlement, channel) => {

    const hashedSecret = Buffer.from(settlement.initiateChannel.hashedSecret, 'hex');

    // 1. audit 'initiate' channel
    const auditAssetType = settlement.initiateChannel.asset;

    const { privateKey: auditPrivateKey } = await AFC.getDerivedKeyPair(cache.getAccessFileSeed(), auditAssetType, 0);
    const auditHtlc = HtlcFactory.create(auditAssetType, auditPrivateKey);
    const auditContractParameters = AuditContractArgsFactory.create(auditAssetType, settlement, hashedSecret);
    const audit = await auditHtlc.auditContract(...auditContractParameters);

    const auditIsValid = auditValidators(auditAssetType)(
      audit,
      settlement.initiateChannel.addressFrom,
      settlement.initiateChannel.addressTo,
      settlement.initiateChannel.amount,
      settlement.initiateChannel.timeout,
      hashedSecret,
    )

    if(!auditIsValid) {
      throw Error('Audit failed! Details: ' + audit);
    }


    // 2. create 'participate' channel
    const participateAssetType = settlement.participateChannel.asset;
    const { privateKey: participatePrivateKey } = await AFC.getDerivedKeyPair(cache.getAccessFileSeed(), participateAssetType, 0);
    const participateHtlc = HtlcFactory.create(participateAssetType, participatePrivateKey);
    const participateParameters = await ParticipateArgsFactory.create(participateAssetType, settlement, hashedSecret, participateHtlc);
    const participateChannelMethod = `participate${capitalizeFirstLetter(participateAssetType)}`;

    const txContractHash = await ParticipateHandler[participateChannelMethod](participateHtlc, participateParameters, settlement);

    // Notify API.
    await API.clearing.settle({
        channelID: channel.channelID,
        customerID: cache.getCustomerID(),
        settlementID: settlement.settlementID,
        hashedSecret: hashedSecret.toString('hex'),
        txContractHash,
    });
};

/**
 * Processes a UTXO (e.g. BTC) type channel initiation.
 */
const registerDelegatedUtxoTxs = async (settlement, participation, fees) => {
    await API.clearing.registerParticipationDelegatedTxsUtxoData({
        customerID: cache.getCustomerID(),
        settlementID: settlement.settlementID,
        redeemScript: participation.redeemScript.toString('hex'),
        refundTransaction: participation.refundTx.toHex(),
        fees,
    });
};

class ParticipateHandler {

  static async participateBtc (
    htlc,
    participateParameters,
    settlement,
  ) {
    const participation = await htlc.participate(...participateParameters);

    // TODO: implement
    // 1. register delegated txs: refund, redeem
    await registerDelegatedUtxoTxs(settlement, participation, participation.fees);

    // 2. relay create channel tx
    return htlc.client.sendRawTransaction(
      participation.createChannelTx.toHex(),
    );
  }

  static async participateEth (
    htlc,
    participateParameters,
    settlement,
  ) {
    console.log(`Sending ${participateParameters[1].toString()} WEI into HTLC, redeemable by ${participateParameters[0]}...`);
    const blockIncludingParticipateTx = await htlc.participate(...participateParameters);

    // TODO: extend schema or remove (see https://github.com/Trinkler/trade-cli/blob/c5297ef03475d8d37b5d1785fe75e9ad45ec1f0e/src/commands/clearing/redeem/redeem.js#L153-L163)
    const channelId = blockIncludingParticipateTx.events.ChannelCreated.returnValues.channelId;
    return blockIncludingParticipateTx.transactionHash;
  }

}
