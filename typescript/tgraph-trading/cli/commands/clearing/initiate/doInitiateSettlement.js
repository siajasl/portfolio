/**
 * @fileOverview Initiates trade settlement process.
 */

import * as bitcoin from 'bitcoinjs-lib';
import * as Utils from 'web3-utils';
import { cache, executor, io, parsing } from '../../../utils';
import { capitalizeFirstLetter } from '../../../utils/capitalizeFirstLetter';
import { API, AFC } from '../../../utils/imports';
import { HtlcFactory } from '../utils/htlcFactory';
import generateSecret from './secretFactory';

/**
 * Establishes swap initiataion channel.
 */
export default async (settlement, channel) => {
    // Generate secret.
    const secret = generateSecret(channel.asset);
    const bufferedSecret = Buffer.from(secret, 'hex');

    // Set hash of secret.
    const hashedSecret = bitcoin.crypto.sha256(bufferedSecret);

    // Derive private key.
    const { privateKey } = await AFC.getDerivedKeyPair(cache.getAccessFileSeed(), channel.asset, 0);

    // initiate channel
    const initiateChannelMethod = `initiate${capitalizeFirstLetter(channel.asset)}`;
    const txContractHash = await InitiateHandler[initiateChannelMethod](channel, settlement, privateKey, secret);

    // 3. Notify API.
    await API.clearing.settle({
        channelID: channel.channelID,
        customerID: cache.getCustomerID(),
        settlementID: settlement.settlementID,
        hashedSecret: hashedSecret.toString('hex'),
        txContractHash,
    });

    // Persist to file system for downstream usage.
    await io.settlements.writeOnInitiation(settlement.settlementID, secret);
};

/**
 * Processes a UTXO (e.g. BTC) type channel initiation.
 */
const registerDelegatedUtxoTxs = async (settlement, initiation, fees) => {
    await API.clearing.registerInitiationDelegatedTxsUtxoData({
        customerID: cache.getCustomerID(),
        settlementID: settlement.settlementID,
        redeemScript: initiation.redeemScript.toString('hex'),
        refundTransaction: initiation.refundTx.toHex(),
        fees,
    });
};

/**
 * DLT-specific handlers for deploying an 'initiate' channel
 */
class InitiateHandler {

  static async initiateBtc (channel, settlement, privateKey, secret) {

    const htlc = HtlcFactory.create('BTC', privateKey)

    const assumedFees = await htlc.getAssumedFees();
    const safeFee = assumedFees.createChannelTx + assumedFees.redeemTx;
    // the channel amount includes all the fees
    const amount = Number(channel.amount);
    const totalAmount = amount + safeFee;
    const minConfirmations = 0;
    const allWatchedUtxos = await htlc.client.listUnspent(minConfirmations);
    const userUtxos = allWatchedUtxos.filter(u => u.address === channel.addressFrom);
    const selectedUtxos = htlc.selectUtxosForAmount(userUtxos, totalAmount);
    const timeout = { utc: Math.round(channel.timeout / 1e3) };
    const fees = await htlc.estimateFees( // TODO: send to API
      channel.addressTo,
      amount,
      secret,
      timeout,
      selectedUtxos,
    );

    const initiation = await htlc.initiate(
      channel.addressTo,
      amount,
      secret,
      timeout,
      selectedUtxos,
      fees,
    );

    // 1. register delegated txs: refund, redeem
    await registerDelegatedUtxoTxs(settlement, initiation, fees);

    // 2. relay create channel tx
    return htlc.client.sendRawTransaction(
      initiation.createChannelTx.toHex(),
    );
  }

  static async initiateEth (channel, settlement, privateKey, secret) {

    const htlc = HtlcFactory.create('ETH', privateKey)

    const amountInEth = channel.amount;
    const amountInWei = Utils.toWei(amountInEth);
    const amount = Utils.toBN(`${amountInWei}`);

    const txReceipt = await htlc.initiate(
      channel.addressTo,
      amount,
      secret,
      channel.timeout,
    );

    // TODO: extend schema or remove (see https://github.com/Trinkler/trade-cli/blob/c5297ef03475d8d37b5d1785fe75e9ad45ec1f0e/src/commands/clearing/redeem/redeem.js#L153-L163)
    const channelId = blockIncludingParticipateTx.events.ChannelCreated.returnValues.channelId;
    return txReceipt.events.ChannelCreated.returnValues.channelId;
  }
}
