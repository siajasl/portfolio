"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const accessfile_tx_1 = require("@trinkler/accessfile-tx");
exports.isEthTxMined = async (txId) => {
    try {
        const tx = await accessfile_tx_1.ethWalletService.getTransaction(txId);
        return tx;
    }
    catch (error) {
        return null;
    }
};
//# sourceMappingURL=is-mined.js.map