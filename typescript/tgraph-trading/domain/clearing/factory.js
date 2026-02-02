/**
 * @fileOverview Maps a set of trades to a set of settlements for clearing.
 */

import { Channel } from './channel';
import { ChannelTypeEnum } from './enums';
import { CounterParties } from './counterParties';
import { CounterParty } from './counterParty';
import { Settlement } from './settlement';

/**
 * Maps trade engine output to clearing engine input.
 * @param {trading.Exchange} exchange - Exchange upon which asset pair is being traded.
 * @param {trading.Trade} trade - A matched trade between counter-parties.
 */
export const getSettlement = (trade) => {
    // Set asset pair being traded.
    const { assetPair, exchange } = trade;

    // Establish counter-parties.
    const counterParties = CounterParties.createFromTrade(trade);

    // Set swap channel 01, i.e. the initiate channel.
    const initiateChannel = getInitiateChannel(exchange, assetPair, counterParties, trade);

    // Set swap channel 02, i.e. the participate channel.
    const participateChannel = getParticipateChannel(exchange, assetPair, counterParties, trade);

    // Set settlement.
    const settlement = new Settlement({ assetPair, counterParties, initiateChannel, participateChannel });

    // X-reference trade & settlement for downstream usage.
    trade.settlementID = settlement.settlementID;

    return settlement;
}

/**
 * Returns initiate channel settlement information.
 */
const getInitiateChannel = (exchange, assetPair, counterParties, trade) => {
    const factory = trade.makeOrder.side === 'SELL' ? getInitiateChannelForAsk :
                                                      getInitiateChannelForBid;

    return factory(exchange, assetPair, counterParties, trade);
}

/**
 * Returns initiate channel ask side settlement information.
 */
const getInitiateChannelForAsk = (exchange, assetPair, counterParties, { makeOrder, takeOrder, quantity }) => {
    return new Channel({
        amount: quantity,
        commission: quantity.multipliedBy(exchange.commissionPercentage),
        counterParties,
        asset: assetPair.base,
        addressFrom: makeOrder.quote.addressOfBaseAsset,
        timeout: getTimeoutInMS(exchange.htlcTimeouts.initiate),
        addressTo: takeOrder.quote.addressOfBaseAsset,
        type: ChannelTypeEnum.INITIATE
    });
}

/**
 * Returns initiate channel bid side settlement information.
 */
const getInitiateChannelForBid = (exchange, assetPair, counterParties, { makeOrder, takeOrder, price, quantity }) => {
    const amount = quantity.multipliedBy(price);
    return new Channel({
        amount,
        commission: amount.multipliedBy(exchange.commissionPercentage),
        counterParties,
        asset: assetPair.quote,
        addressFrom: makeOrder.quote.addressOfQuoteAsset,
        timeout: getTimeoutInMS(exchange.htlcTimeouts.initiate),
        addressTo: takeOrder.quote.addressOfQuoteAsset,
        type: ChannelTypeEnum.INITIATE
    });
}

/**
 * Returns participate channel settlement information.
 */
const getParticipateChannel = (exchange, assetPair, counterParties, trade) => {
    const factory = trade.takeOrder.side === 'SELL' ? getParticipateChannelForAsk :
                                                      getParticipateChannelForBid;

    return factory(exchange, assetPair, counterParties, trade);
}

/**
 * Returns participate channel ask side settlement information.
 */
const getParticipateChannelForAsk = (exchange, assetPair, counterParties, { makeOrder, takeOrder, quantity }) => {
    return new Channel({
        amount: quantity,
        commission: quantity.multipliedBy(exchange.commissionPercentage),
        counterParties,
        asset: assetPair.base,
        addressFrom: takeOrder.quote.addressOfBaseAsset,
        timeout: getTimeoutInMS(exchange.htlcTimeouts.participate),
        addressTo: makeOrder.quote.addressOfBaseAsset,
        type: ChannelTypeEnum.PARTICIPATE
    });
}

/**
 * Returns participate channel bid side settlement information.
 */
const getParticipateChannelForBid = (exchange, assetPair, counterParties, { makeOrder, takeOrder, price, quantity }) => {
    const amount = quantity.multipliedBy(price);
    return new Channel({
        amount,
        commission: amount.multipliedBy(exchange.commissionPercentage),
        counterParties,
        asset: assetPair.quote,
        addressFrom: takeOrder.quote.addressOfQuoteAsset,
        timeout: getTimeoutInMS(exchange.htlcTimeouts.participate),
        addressTo: makeOrder.quote.addressOfQuoteAsset,
        type: ChannelTypeEnum.PARTICIPATE
    });
}

/**
 * Returns moment in unix time when HTLC is considered to have timed out.
 */
const getTimeoutInMS = (offsetInSeconds) => {
    const nowInMilliseconds = Date.now();
    const offsetInMilliseconds = offsetInSeconds * 1000;

    return nowInMilliseconds + offsetInMilliseconds;
}
