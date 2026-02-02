export class AuditContractArgsFactory {
  static create(assetType, settlement, hashedSecret) {
    switch (assetType) {
      case 'BTC':
        return [
          settlement.initiateChannel.txContract.hash, // create channel tx id
          settlement.initiateChannel.addressFrom, // refund address
          settlement.initiateChannel.addressTo, // redeem address
          { utc: Number(Math.round(settlement.initiateChannel.timeout / 1e3 )) }, // expiration time
          hashedSecret, // hashed secret
          settlement.initiateChannel.amount, // amount
        ];
      case 'ETH':
        return [
          settlement.initiateChannel.txContract.hash,
        ];
      default:
          throw Error(`Asset type ${assetType} is not supported`);
    }
  }
}
