/**
* @fileOverview Maps a trading enging layer struct to a GQL layer struct.
 */

import * as _ from 'lodash';

/**
 * Maps a trading engine layer struct to a dB layer struct.
 */
export default (book) => {
    return {
        assetPair: book.assetPair.symbol,
        asks: mapOrderTree(book.asks),
        bids: mapOrderTree(book.bids).reverse(),
        exchange: book.exchange.symbol
    }
};

/**
 * Maps an order tree to required output.
 */
const mapOrderTree = (orderTree) => {
    return _.map(orderTree.priceMap.toArray(), (orderList) => {
        return {
            price: orderList.price,
            quantity: orderList.getVolume(),
            orders: mapOrderList(orderList)
        }
    });
}

const mapOrderList = (orderList) => {
    const result = [];
    if (orderList.head === null) {
        return result;
    }

    let order = orderList.head;
    do {
        result.push(mapOrder(order));
        order = order.next;
    } while (order !== null);

    return result;
}

/**
 * Maps an order to required output.
 */
const mapOrder = (order) => {
    return {
        price: order.quote.price,
        quantity: order.unfilled,
        side: order.quote.side,
        type: order.quote.type,
        uid: order.orderID
    }
}
