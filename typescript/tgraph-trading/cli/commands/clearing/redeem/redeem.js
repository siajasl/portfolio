// Copyright 2019 Trinkler Software AG (Switzerland).
// Trinkler Software provides free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version <http://www.gnu.org/licenses/>.
import * as bitcoin from 'bitcoinjs-lib';

/**
 * @fileOverview Redeems a settlement channel.
 */
import { ExtractSecretArgsFactory } from '.';
import { cache, executor, parsing, io } from '../../../utils';
import { capitalizeFirstLetter } from '../../../utils/capitalizeFirstLetter';
import { API, AFC } from '../../../utils/imports';
import { HtlcFactory } from '../utils/htlcFactory';

/**
 * Command execution entry point.
 */
const execute = async ({ settlementId: settlementID }, opts) => {
    // Pull.
    const settlement = await API.clearing.getSettlement({
        customerID: cache.getCustomerID(),
        settlementID
    });

    // Validate.
    validate(settlement);

    // Execute.
    if (settlement.counterPartyOneID === cache.getCustomerID()) {
        await doRedeemChannelTwo(settlement, settlement.participateChannel);
    } else {
        await doRedeemChannelOne(settlement, settlement.initiateChannel);
    }};

/**
 * Validates settlement pulled from API.
 */
const validate = (settlement) => {
    if (settlement === null) {
        throw new Error('Either settlement does not exist or you are not a settlement counter-party');
    }

    if (settlement.counterPartyOneID === cache.getCustomerID()) {
        if (settlement.state !== 'ParticipateRedeemAwaiting') {
            throw new Error('settlement status does not support channel redemption');
        }
        // TODO validate that settlement secret file exists.
    }

    if (settlement.counterPartyTwoID === cache.getCustomerID()) {
        if (settlement.state !== 'InitiateRedeemAwaiting') {
            throw new Error('settlement status does not support channel redemption');
        }
    }
};

/**
 * Redeems channel two (by initiator).
 * Secret is known
 */
const doRedeemChannelTwo = async (settlement, channel) => {
    const assetType = channel.asset;

    const { privateKey } = await AFC.getDerivedKeyPair(cache.getAccessFileSeed(), assetType, 0);
    const htlc = HtlcFactory.create(assetType, privateKey);

    const secret = await io.settlements.readRedemptionSecret(settlement.settlementID);

    const redeemMethod = `redeem${capitalizeFirstLetter(assetType)}`;

    await RedeemHandler[redeemMethod](htlc, channel, secret);
}

/**
 * Redeems channel one (by participant).
 * Secret has to be extracted
 */
const doRedeemChannelOne = async (settlement, channel) => {

  // extract secret
  const extractSecretAssetType = settlement.participateChannel.asset;

  const { privateKey: extractSecretPrivateKey } = await AFC.getDerivedKeyPair(cache.getAccessFileSeed(), extractSecretAssetType, 0);
  const extractSecretHtlc = HtlcFactory.create(extractSecretAssetType, extractSecretPrivateKey);
  const extractSecretArgs = await ExtractSecretArgsFactory.create(extractSecretHtlc, extractSecretAssetType, settlement);
  const secret = await extractSecretHtlc.extractSecret(...extractSecretArgs);

  // redeem
  const redeemAssetType = channel.asset;
  const { privateKey: redeemEntropyPrivateKey } = await AFC.getDerivedKeyPair(cache.getAccessFileSeed(), redeemAssetType, 0);
  const redeemHtlc = HtlcFactory.create(redeemAssetType, redeemEntropyPrivateKey);

  const redeemMethod = `redeem${capitalizeFirstLetter(redeemAssetType)}`;

  await RedeemHandler[redeemMethod](redeemHtlc, channel, secret.replace('0x', ''));
}

class RedeemHandler {

  static async redeemBtc (
    htlc,
    channel, // from settlement.{participate|initiate}Channel
    secret, // if 2nd party, extract secret before calling this function
  ) {
    // fee estimation
    // TODO: consider extending the schema by adding the participate channel fees
    // (as it's already implemented in case of the initiate channel)
    const assumedFee = 0.00001;
    const assumedRedeemTx = await htlc.redeem(
      channel.txContract.hash,
      Buffer.from(channel.txRedeem.script, 'hex'),
      secret,
      htlc.keyPair,
      assumedFee,
    );
    const redeemFee = await htlc.estimateFeeForTx(
      bitcoin.Transaction.fromHex(assumedRedeemTx)
    );

    // build redeem tx
    const redeemTx = await htlc.redeem(
      channel.txContract.hash,
      Buffer.from(channel.txRedeem.script, 'hex'),
      secret,
      htlc.keyPair,
      redeemFee,
    );

    // send redeem tx to network
    return htlc.client.sendRawTransaction(redeemTx);
  }

  static async redeemEth (
    htlc,
    channel,
    secret,
  ) {
    const conditional0xPrefix = secret.substring(0, 2) !== '0x' ? '0x' : '';

    // get channelCreated event
    // TODO: either save the channel id to the schema during initiation, see:
    // - https://github.com/Trinkler/trade-cli/blob/feature/redeem/src/commands/clearing/initiate/doInitiateSettlement.js#L128-L129 and
    // - https://github.com/Trinkler/trade-cli/blob/feature/redeem/src/commands/clearing/participate/doParticipateSettlement.js#L115-L116
    // OR use this solution to avoid downloading all 'channel created' events for each swap https://ethereum.stackexchange.com/a/66459
    const allChannelCreatedEvents = await htlc.contract.getPastEvents('ChannelCreated',
      {
        fromBlock: 7495723, // creation of contract, see https://etherscan.io/txs?a=0x0e73fac981d22a0511b42f757186c3514ce60c4e
        toBlock: 'latest'
      }
    );
    const channelCreatedEvent = allChannelCreatedEvents.find(e => e.transactionHash === channel.txContract.hash);
    const channelId = channelCreatedEvent.returnValues.channelId;

    return htlc.redeem(
      channelId,
      `${conditional0xPrefix}${secret}`,
    )
  }
}

// Export command.
export default (program) => {
    program
        .command('redeem', 'Redeem a settlement channel')
        .argument('<settlementId>', 'Settlement ID', parsing.parseUUID)
        .action((args, opts) => executor(execute, args, opts));
}
