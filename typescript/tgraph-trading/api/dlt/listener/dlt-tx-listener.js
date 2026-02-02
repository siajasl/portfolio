"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const filter_engine_1 = require("./filter-engine");
class TxListenerBase {
    constructor() {
        this.filterEngine = new filter_engine_1.ListenerFilterEngine();
        this.addTx = (txId) => {
            this.filterEngine.addTx(txId);
            this.filterEngine.emitFilteredTransactions();
        };
        this.removeTx = (txId) => {
            this.filterEngine.removeTx(txId);
            this.filterEngine.emitFilteredTransactions();
        };
    }
}
exports.TxListenerBase = TxListenerBase;
//# sourceMappingURL=dlt-tx-listener.js.map