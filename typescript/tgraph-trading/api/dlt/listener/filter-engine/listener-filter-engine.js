"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const rxjs_1 = require("rxjs");
class ListenerFilterEngine {
    constructor() {
        /**
         * contains txs that are being listened for
         */
        this.listeningTxs = [];
        /**
         * RxJs subject that emits the txs that are being listened for whenever their state changes
         */
        this.listeningTxsSubject = new rxjs_1.Subject();
        /**
         * adds a transaction id to the filtered txs array
         */
        this.addTx = (txId) => (this.listeningTxs = [...this.listeningTxs, txId]);
        /**
         * remove a transaction id from the filtered txs array
         */
        this.removeTx = (tx) => {
            const txIndex = this.listeningTxs.indexOf(tx);
            if (txIndex === -1) {
                return;
            }
            this.listeningTxs = this.listeningTxs.slice(0, txIndex)
                .concat(this.listeningTxs.slice(txIndex + 1));
        };
        /**
         * emits the current txs that are being listened for to all subscribers of 'listeningTxsSubject'
         */
        this.emitFilteredTransactions = () => this.listeningTxsSubject.next(this.listeningTxs);
    }
}
exports.ListenerFilterEngine = ListenerFilterEngine;
//# sourceMappingURL=listener-filter-engine.js.map