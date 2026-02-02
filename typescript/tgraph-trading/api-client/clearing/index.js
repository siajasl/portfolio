/**
 * @fileOverview Sub-package entry point.
 */

import { eventEmitter as events } from './utils';

import getChannel from './getChannel';
import getOpenSettlements from './getOpenSettlements';
import getSettlement from './getSettlement';
import getSettlementList from './getSettlementList';

import onSettlementComplete from './onSettlementComplete';
import onSettlementFinalise from './onSettlementFinalise';
import onSettlementInitiate from './onSettlementInitiate';
import onSettlementParticipate from './onSettlementParticipate';
import onSettlementStateChange from './onSettlementStateChange';

import registerInitiationDelegatedTxsUtxoData from './doRegisterDelegatedTxsUtxoDataOnInitiateChannel';
import registerParticipationDelegatedTxsUtxoData from './doRegisterDelegatedTxsUtxoDataOnParticipateChannel';
import registerSecret from './doRegisterSecret';
import settle from './doSettle';

export {
    // event emitter
    events,

    // mutations
    registerInitiationDelegatedTxsUtxoData,
    registerParticipationDelegatedTxsUtxoData,
    registerSecret,
    settle,

    // queries
    getChannel,
    getOpenSettlements,
    getSettlement,
    getSettlementList,

    // subscriptions
    onSettlementComplete,
    onSettlementFinalise,
    onSettlementInitiate,
    onSettlementParticipate,
    onSettlementStateChange
};
