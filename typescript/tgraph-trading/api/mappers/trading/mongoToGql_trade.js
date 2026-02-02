/**
 * @fileOverview Maps a trading engine layer struct to a dB layer struct.
 */

/**
 * Maps a dB layer struct to a GQL layer struct.
 */
export default (customerID, trade) => {
    // Destructure dB struct.
    const {
        exchangeSymbol: exchange,
        makeOrderID,
        makeSide,
        takeCustomerID,
        takeOrderID,
        takeSide
    } = trade;

    const orderID = (customerID === takeCustomerID) ? takeOrderID : makeOrderID;
    const side = (customerID === takeCustomerID) ? takeSide : makeSide;

    // Return GQL struct.
    return {
        customerID,
        exchange,
        orderID,
        side,
        ...trade._doc
    };
};
