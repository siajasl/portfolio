/**
 * @fileOverview Maps a trading engine layer struct to a dB layer struct.
 */

/**
 * Maps a dB layer struct to a GQL layer struct.
 */
export default (order, trades) => {
    // Destructure dB struct.
    const {
        exchangeSymbol: exchange
    } = order;

    // Return GQL struct.
    return {
        exchange,
        trades: (trades || []).map(i => i._doc),
        ...order._doc
    };
};
