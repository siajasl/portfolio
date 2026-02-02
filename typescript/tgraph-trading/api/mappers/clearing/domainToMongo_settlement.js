/**
 * @fileOverview An inter-layer struct mapper.
 */

/**
 * Maps a clearing settlement to it's dB layer struct representation.
 */
export default (settlement) => {
    // Destructure clearing engine struct.
    const {
        assetPair: {
            symbol: assetPair
        },
        counterParties: {
            partyOne: {
                customerID: counterPartyOneID
            },
            partyTwo: {
                customerID: counterPartyTwoID
            }
        },
        state,
        settlementID,
    } = settlement;

    // Return dB struct.
    return {
        assetPair,
        counterPartyOneID,
        counterPartyTwoID,
        initiateChannel: mapChannel(settlement.initiateChannel, 'INITIATE'),
        participateChannel: mapChannel(settlement.participateChannel, 'PARTICIPATE'),
        settlementID,
        state,
        stateHistory: [
            {
                state,
            }
        ],
    };
};

/**
 * Maps a clearing channel to it's dB layer struct representation.
 */
const mapChannel = (channel, type) => {
    const {
        addressFrom,
        addressTo,
        amount,
        channelID,
        commission,
        hashedSecret,
        timeout,
        asset: {
            symbol: asset
        }
    } = channel;

    return {
        addressFrom,
        addressTo,
        asset,
        amount,
        channelID,
        commission,
        hashedSecret,
        timeout,
        txContract: mapTransaction(channel.txContract, 'CONTRACT'),
        txRedeem: mapTransaction(channel.txRedeem, 'REDEEM'),
        txRefund: mapTransaction(channel.txRefund, 'REFUND'),
        type
    }
}

/**
 * Maps a clearing transaction to it's dB layer struct representation.
 */
const mapTransaction = (transaction, type) => {
    const {
        asset: {
            symbol: asset
        },
        hash,
        script,
        secret,
        signature,
        state,
        transactionID
    } = transaction;

    return {
        asset,
        hash,
        script,
        secret,
        signature,
        state,
        stateHistory: [
            {
                state,
            }
        ],
        transactionID,
        type
    };
}
