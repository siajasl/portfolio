/**
 * @fileOverview Adds an order to a book.
 */

import { CONSTANTS } from '../../utils/imports';
import compose from '../shared/resolvers/factory';
import mapDomainOutputsToGQLOutput from '../../mappers/trading/domainToGql_newOrder';
import mapGQLInputToQuote from '../../mappers/trading/gqlToDomain_quote';
import mapOrderToMongoDocument from '../../mappers/trading/domainToMongo_order';
import mapTradeToMongoDocument from '../../mappers/trading/domainToMongo_trade';
import mapSettlementToMongoDocument from '../../mappers/clearing/domainToMongo_settlement';
import onBookUpdate from './onBookUpdate';
import onOrderFill from './onOrderFill';
import onSettlementInitiate from '../clearing/onSettlementInitiate';
import onSettlementStateChange from '../clearing/onSettlementStateChange';

// Destructure enums.
const { clearing : { SettlementStateEnum} } = CONSTANTS;

/**
 * GQL endpoint resolver: trading.addOrder.
 */
const resolve = async (parent, { input }, { clearing, dbe, exchanges, pubsub }, info) => {
    // Set exchange.
    const exchange = exchanges.find(i => i.symbol === input.exchange);

    // Match.
    const quote = mapGQLInputToQuote(input, exchange);
    const { book, order, trades } = exchange.submitOrder(quote);

    // Clear.
    const settlements = trades.map(trade => clearing.getSettlement(trade));

    // Persist.
    await persistTradeEngineOutput(dbe, order, trades, settlements);

    // Publish.
    await publishTradeEngineEvents(pubsub, book, order, trades)

    // Initiate settlements.
    await initiateSettlements(dbe, pubsub, settlements);

    // Return new order information.
    return mapDomainOutputsToGQLOutput(quote, order, trades);
};

/**
 * Persists trade engine outputs to dB.
 */
const persistTradeEngineOutput = async (dbe, order, trades, settlements) => {
    // Persist new order.
    await dbe.addOrder(mapOrderToMongoDocument(order));

    // Persist existing order state changes.
    const orders = new Set(trades.map(i => i.makeOrder));
    for (const { orderID, state, filled: quantityFilled } of orders) {
        await dbe.updateOrder({ orderID }, { quantityFilled, state });
    }

    // Persist trades.
    await dbe.addTrades(trades.map(mapTradeToMongoDocument));

    // Persist settlements.
    await dbe.addSettlements(settlements.map(mapSettlementToMongoDocument));
}

/**
 * Publishes post trade events.
 */
const publishTradeEngineEvents = async (pubsub, book, order, trades) => {
    // Publish event: onBookUpdate.
    if (order !== null || trades.length > 0) {
        await onBookUpdate.publish(pubsub, { book });
    }

    // Publish event: onOrderFill.
    for (const trade of trades) {
        // Publish to order maker.
        await onOrderFill.publish(pubsub, {
            customerID: trade.makeOrder.customerID,
            customerReferenceID: trade.makeOrder.quote.customerReferenceID,
            orderID: trade.makeOrder.orderID,
            settlementID: trade.settlementID,
            tradeID: trade.tradeID
        });

        // Publish to order taker.
        await onOrderFill.publish(pubsub, {
            customerID: trade.takeOrder.customerID,
            customerReferenceID: trade.takeOrder.quote.customerReferenceID,
            orderID: trade.takeOrder.orderID,
            settlementID: trade.settlementID,
            tradeID: trade.tradeID
        });
    }
}

/**
 * Initiates settlement clearing process.
 */
const initiateSettlements = async (dbe, pubsub, settlements) => {
    for (const settlement of settlements) {
        // Update.
        settlement.state = SettlementStateEnum.InitiateSetupAwaiting;

        // Persist.
        await dbe.updateSettlementState(settlement);

        // Destructure.
        const { settlementID, state, counterParties } = settlement;

        // Publish event: onSettlementStateChange.
        for (const { customerID } of [counterParties.partyOne, counterParties.partyTwo]) {
            await onSettlementStateChange.publish(pubsub, {
                customerID,
                settlementID,
                state
            });
        }

         // Publish event: onSettlementInitiate (cp1 only).
         await onSettlementInitiate.publish(pubsub, {
             settlementID,
             customerID: counterParties.partyOne.customerID
         });
    }
}

// Export composed resolver.
export default compose(resolve, 'add order');
