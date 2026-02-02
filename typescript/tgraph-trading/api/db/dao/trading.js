import { CONSTANTS } from '../../utils/imports';

// Destructure enums.
const { trading : { OrderStateEnum } } = CONSTANTS;

/**
 * @fileOverview Trading related data access operations.
 */

/**
 * Inserts an order into dB.
 */
export const addOrder = async ({ Order }, obj) => {
    return await Order.create(obj);
}

/**
 * Inserts a trade into dB.
 */
export const addTrade = async ({ Trade }, obj) => {
    return await Trade.create(obj);
}

/**
 * Inserts a collection of trades into dB.
 */
export const addTrades = async ({ Trade }, trades) => {
    // TODO: optimise by persisting a batch.
    for (const trade of trades) {
        await Trade.create(trade);
    }
}

/**
 * Returns asset meta-data.
 */
export const getAssets = async ({ Asset }) => {
    return await Asset.find();
}

/**
 * Returns exchange meta-data.
 */
export const getExchanges = async ({ Exchange }) => {
    return await Exchange.find();
}

/**
 * Returns orders filtered by customer identifier.
 */
export const getOpenOrders = async ({ Order }, { customerID }) => {
    const qry = {
        '$or': [
            { state: OrderStateEnum.NEW },
            { state: OrderStateEnum.PARTIALLY_FILLED },
            { state: OrderStateEnum.UNFILLED }
        ]
    }
    if (customerID) {
        qry['customerID'] = customerID;
    }

    return await Order.find(qry);
}

/**
 * Returns an order matched by customer/order identifier.
 */
export const getOrder = async ({ Order }, { customerID, orderID }) => {
    return await Order.findOne({
        customerID,
        orderID,
    });
}

/**
 * Returns trades filtered by order identifier.
 */
export const getTradesByOrder = async ({ Trade }, { orderID }) => {
    return await Trade.find({
        '$or': [
            { makeOrderID: orderID },
            { takeOrderID: orderID }
        ]
    });
}

/**
 * Returns trades filtered by customer identifier & timespan.
 */
export const getTradeHistory = async ({ Trade }, {
    assetPair,
    state,
    customerID,
    dateFrom,
    dateTo
}) => {
    const qry = {
        '$or': [
            { makeCustomerID: customerID },
            { takeCustomerID: customerID }
        ]
    };

    if (assetPair) {
        qry['assetPair'] = assetPair;
    }

    if (state) {
        qry['state'] = state;
    }

    if (dateFrom && dateTo) {
        qry['$and'] = [
            { timestamp: { $gt: dateFrom } },
            { timestamp: { $lt: dateTo } }
        ]
    } else if (dateFrom) {
        qry['$and'] = [
            { timestamp: { $gt: dateFrom } }
        ]
    } else if (dateTo) {
        qry['$and'] = [
            { timestamp: { $lt: dateFrom } }
        ]
    }

    return await Trade.find(qry);
}


/**
 * Updates an order.
 */
export const updateOrder = async ({ Order }, filter, obj) => {
    await Order.updateOne(filter, obj);
}

/**
 * Upserts an asset into dB.
 */
export const upsertAsset = async ({ Asset }, obj) => {
    const { symbol } = obj;

    return await Asset.findOneAndUpdate({ symbol }, obj, { upsert:true });
}

/**
 * Upserts an exchange into dB.
 */
export const upsertExchange = async ({ Exchange }, obj) => {
    const { exchangeID } = obj;

    return await Exchange.findOneAndUpdate({ exchangeID }, obj, { upsert:true });
}
