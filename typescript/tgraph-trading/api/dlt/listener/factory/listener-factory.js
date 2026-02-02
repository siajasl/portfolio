"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const accessfile_tx_1 = require("@trinkler/accessfile-tx");
const __1 = require("..");
const btc_tx_listener_1 = require("../btc-tx-listener");
class ListenerFactory {
    constructor() {
        this.listeners = [];
        this.getInstance = (assetClass, listenerType) => this.getExistingInstance(assetClass, listenerType) ||
            this.getNewInstance(assetClass, listenerType);
        this.getExistingInstance = (assetClass, listenerType) => {
            const listenerDefinition = this.listeners.find(l => l.assetClass === assetClass && l.listenerType === listenerType);
            if (!listenerDefinition) {
                return null;
            }
            return listenerDefinition.listener;
        };
        this.getNewInstance = (assetClass, listenerType) => {
            if (this.getExistingInstance(assetClass, listenerType)) {
                return this.getExistingInstance(assetClass, listenerType);
            }
            switch (assetClass) {
                case accessfile_tx_1.AssetClass.BTC: {
                    this.addInstance({
                        assetClass: assetClass,
                        listenerType: listenerType,
                        listener: new btc_tx_listener_1.BtcTxListener(),
                    });
                    break;
                }
                case accessfile_tx_1.AssetClass.ETH: {
                    this.addInstance({
                        assetClass: assetClass,
                        listenerType: listenerType,
                        listener: new __1.EthTxListener(),
                    });
                    break;
                }
                default: {
                    throw new Error(`AssetType ${assetClass} is not defined!`);
                }
            }
            return this.getExistingInstance(assetClass, listenerType);
        };
        this.addInstance = (listener) => this.listeners = [
            ...this.listeners,
            listener,
        ];
    }
}
exports.listenerFactory = new ListenerFactory();
//# sourceMappingURL=listener-factory.js.map