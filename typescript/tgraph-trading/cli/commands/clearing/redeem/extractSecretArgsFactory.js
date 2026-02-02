export class ExtractSecretArgsFactory {
  static async create(htlc, assetType, settlement) {
    console.log("TCL: ExtractSecretArgsFactory -> staticcreate -> settlement.participateChannel.txRedeem.hash", settlement.participateChannel.txRedeem.hash)

    switch (assetType) {
      case 'BTC':
        return [
          await htlc.client.getRawTransaction(settlement.participateChannel.txRedeem.hash, true)
        ];
      case 'ETH':
        return [
          settlement.participateChannel.txRedeem.hash
        ];
      default:
        throw Error(`Asset type ${assetType} is not supported`);
    }
  }
}
