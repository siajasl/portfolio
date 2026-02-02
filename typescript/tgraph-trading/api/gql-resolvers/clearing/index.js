/**
 * @fileOverview Settlement engine endpoints.
 */

import registerInitiationDelegatedTxsUtxoData from './doRegisterDelegatedTxsUtxoDataOnInitiateChannel';
import registerParticipationDelegatedTxsUtxoData from './doRegisterDelegatedTxsUtxoDataOnParticipateChannel';
import registerSettlementSecret from './doRegisterSecret';
import settle from './doSettle';

import getChannel from './getChannel';
import getOpenSettlements from './getOpenSettlements';
import getSettlement from './getSettlement';
import getSettlementList from './getSettlementList';

import onSettlementComplete from './onSettlementComplete';
import onSettlementFinalise from './onSettlementFinalise';
import onSettlementInitiate from './onSettlementInitiate';
import onSettlementParticipate from './onSettlementParticipate';
import onSettlementStateChange from './onSettlementStateChange';

// Mutation set.
export const mutation = {
    registerInitiationDelegatedTxsUtxoData,
    registerParticipationDelegatedTxsUtxoData,
    registerSettlementSecret,
    settle
}

// Query set.
export const query = {
    getChannel,
    getOpenSettlements,
    getSettlement,
    getSettlementList,
}

// Subscription set.
export const subscription = {
    onSettlementComplete,
    onSettlementFinalise,
    onSettlementInitiate,
    onSettlementParticipate,
    onSettlementStateChange,
}

// Super set.
export const all = {
    registerInitiationDelegatedTxsUtxoData,
    registerSettlementSecret,
    settle,

    getChannel,
    getOpenSettlements,
    getSettlement,
    getSettlementList,

    onSettlementInitiate,
    onSettlementParticipate,
    onSettlementFinalise,
    onSettlementComplete,
    onSettlementStateChange,
}
