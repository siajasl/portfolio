import { CONSTANTS } from '../../utils/imports';

// Destructure enums.
const { clearing : { SettlementStateEnum, TransactionStateEnum } } = CONSTANTS;

/**
 * @fileOverview Clearing related data access operations.
 */

/**
 * Inserts a collection of settlements into dB.
 */
export const addSettlements = async ({ Settlement }, settlements) => {
    await Settlement.create(settlements);
}

/**
 * Returns a settlement matched by customer & settlement identifiers.
 */
export const getSettlement = async ({ Settlement }, { customerID, settlementID }) => {
    return await Settlement.findOne({
        settlementID,
        '$or': [
            { counterPartyOneID: customerID },
            { counterPartyTwoID: customerID }
        ]
    });
}

/**
* Returns a settlement matched by customer & channel identifiers.
 */
export const getSettlementByChannel = async ({ Settlement }, { customerID, channelID }) => {
    return await Settlement.findOne({
        $or: [
            { counterPartyOneID: customerID },
            { counterPartyTwoID: customerID }
        ],
        $or: [
            { 'initiateChannel.channelID': channelID },
            { 'participateChannel.channelID': channelID },
        ]
    });
}

/**
* Returns a settlement document matched by customer & transaction identifiers.
 */
export const getSettlementByTxHash = async ({ Settlement }, txHash) => {
    return await Settlement.findOne({
        $or: [
            { 'initiateChannel.txContract.hash': txHash },
            { 'initiateChannel.txRefund.hash': txHash },
            { 'initiateChannel.txRedeem.hash': txHash },
            { 'participateChannel.txContract.hash': txHash },
            { 'participateChannel.txRefund.hash': txHash },
            { 'participateChannel.txRedeem.hash': txHash },
        ]
    });
}

/**
 * Returns settlements filtered by customer/state/asset identifiers.
 */
export const getSettlementList = async ({ Settlement }, filter) => {
    // Destructure.
    const { customerID, state, symbol: assetPair } = filter;

    // Set query.
    const qry = {
        '$or': [
            { counterPartyOneID: customerID },
            { counterPartyTwoID: customerID }
        ]
    };
    if (state) {
        qry['$and'] = [
            { state },
        ]
    }
    if (assetPair) {
        qry['$and'] = [
            { assetPair },
        ]
    }

    return await Settlement.find(qry);
}

/**
 * Returns open settlements filtered by customer/asset identifiers.
 */
export const getOpenSettlements = async ({ Settlement }, { customerID }) => {
    // Set array of open states.
    const states = [
        SettlementStateEnum.InitiateSetupAwaiting,
        SettlementStateEnum.InitiateSetupDone,
        SettlementStateEnum.ParticipateSetupAwaiting,
        SettlementStateEnum.ParticipateSetupDone,
    ];

    // Set query.
    const qry = {
        '$or': [
            { counterPartyOneID: customerID },
            { counterPartyTwoID: customerID }
        ],
        '$and': [
            {
                state: {
                    $in: states
                }
            }
        ]
    };

    return await Settlement.find(qry);
}

/**
 * Returns all settlements with >1 transactions in a pending state.
 */
export const getSettlementWithPendingTxList = async ({ Settlement }) => {
    return await Settlement.find({
        $or: [
            { 'initiateChannel.txContract.state': TransactionStateEnum.PENDING },
            { 'initiateChannel.txRefund.state': TransactionStateEnum.PENDING },
            { 'initiateChannel.txRedeem.state': TransactionStateEnum.PENDING },
            { 'participateChannel.txContract.state': TransactionStateEnum.PENDING },
            { 'participateChannel.txRefund.state': TransactionStateEnum.PENDING },
            { 'participateChannel.txRedeem.state': TransactionStateEnum.PENDING },
        ]
    });
}

/**
 * Updates a row within a dB collection.
 */
export const updateSettlement = async ({ Settlement }, filter, obj) => {
    await Settlement.updateOne(filter, obj);
}

/**
 * Updates a row within a dB collection.
 */
export const updateSettlementState = async ({ Settlement }, { settlementID, state }) => {
    await Settlement.updateOne({
        settlementID
    }, {
        $push: {
            stateHistory: {
                state: state
            },
        },
        $set: {
            state: state
        }
    });
}
