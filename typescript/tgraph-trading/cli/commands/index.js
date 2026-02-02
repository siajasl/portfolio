/**
 * @fileOverview Entry point.
 */

// clearing related commands
import redeem from './clearing/redeem/redeem';
import settle from './clearing/settle';
import settleAsync from './clearing/settleAsync';
import viewOpenSettlements from './clearing/viewOpenSettlements';
import viewSettlement from './clearing/viewSettlement';
import viewSettlementChannel from './clearing/viewSettlementChannel';
import viewSettlementEvents from './clearing/viewSettlementEvents';
import viewSettlementHistory from './clearing/viewSettlementHistory';
import viewSettlementStates from './clearing/viewSettlementStates';

// settings related commands
import setAccessFile from './settings/setAccessFile';
import setAssetAddress from './settings/setAssetAddress';
import setTradingPreference from './settings/setTradingPreference';
import viewAssetPairList from './settings/viewAssetPairList';
import viewExchangeList from './settings/viewExchangeList';
import viewSettings from './settings/viewSettings';

// trading related commands
import addStandardOrder from './trading/addStandardOrder';
import addMerchantOrder from './trading/addMerchantOrder';
import addOtcOrder from './trading/addOtcOrder';
import cancelOrder from './trading/cancelOrder';

import viewBook from './trading/viewBook';
import viewOpenOrders from './trading/viewOpenOrders';
import viewOrder from './trading/viewOrder';
import viewTradeList from './trading/viewTradeList';
import viewTradingEvents from './trading/viewTradingEvents';

export {
    // clearing
    redeem,
    settle,
    settleAsync,
    viewOpenSettlements,
    viewSettlement,
    viewSettlementEvents,
    viewSettlementChannel,
    viewSettlementHistory,
    viewSettlementStates,

    // settings
    setAccessFile,
    setAssetAddress,
    setTradingPreference,
    viewAssetPairList,
    viewExchangeList,
    viewSettings,

    // trading
    addStandardOrder,
    addMerchantOrder,
    addOtcOrder,
    cancelOrder,

    viewBook,
    viewOpenOrders,
    viewOrder,
    viewTradeList,
    viewTradingEvents,
}
