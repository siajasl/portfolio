/**
 * @fileOverview Sub-package entry point.
 */

import {
    addCorporateCustomer,
    addIndividualCustomerLevel1,
    addIndividualCustomerLevel2,
    getCorporateCustomer,
    getCustomerInfo,
    getIndividualCustomer,
    updateIndividualCustomer,
} from './kyc';

import {
    addSettlements,
    getOpenSettlements,
    getSettlement,
    getSettlementByChannel,
    getSettlementByTxHash,
    getSettlementList,
    getSettlementWithPendingTxList,
    updateSettlement,
    updateSettlementState
} from './clearing';

import {
    addOrder,
    addTrade,
    addTrades,
    getAssets,
    getExchanges,
    getOrder,
    getOpenOrders,
    getTradeHistory,
    getTradesByOrder,
    updateOrder,
    upsertAsset,
    upsertExchange,
} from './trading';

export {
    addCorporateCustomer,
    addIndividualCustomerLevel1,
    addIndividualCustomerLevel2,
    getCorporateCustomer,
    getCustomerInfo,
    getIndividualCustomer,
    updateIndividualCustomer,

    addSettlements,
    getOpenSettlements,
    getSettlement,
    getSettlementByChannel,
    getSettlementByTxHash,
    getSettlementList,
    getSettlementWithPendingTxList,
    updateSettlement,
    updateSettlementState,

    addOrder,
    addTrade,
    addTrades,
    getAssets,
    getExchanges,
    getOpenOrders,
    getOrder,
    getTradeHistory,
    getTradesByOrder,
    updateOrder,
    upsertAsset,
    upsertExchange,
}
