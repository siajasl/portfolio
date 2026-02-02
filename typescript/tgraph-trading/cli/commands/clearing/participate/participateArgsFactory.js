import * as Utils from 'web3-utils';

export class ParticipateArgsFactory {
  static async create(assetType, settlement, hashedSecret, htlc) {
    switch (assetType) {
      case 'BTC': {
        const channel = settlement.participateChannel;

        const allWatchedUtxos = await htlc.client.listUnspent(0);
        const userUtxos = allWatchedUtxos.filter(u => u.address === channel.addressFrom);

        try {
          const assumedFees = await htlc.getAssumedFees();
          const safeFee = assumedFees.createChannelTx + assumedFees.redeemTx;
          const totalAmount = Number(channel.amount) + safeFee;

          const utxos = htlc.selectUtxosForAmount(userUtxos, totalAmount);
          const amount = Number(totalAmount);
          const timeout = { utc: Math.round(Number(channel.timeout) / 1e3) };
          const fees = await htlc.estimateFees(
            channel.addressTo,
            amount,
            hashedSecret,
            timeout,
            utxos,
          );

          return [
            channel.addressTo,
            amount,
            hashedSecret,
            timeout,
            utxos,
            fees,
          ];
        } catch (error) {
          console.error('Failed to produce htlc parameters: ', error);
        }
      }

      case 'ETH':
        return [
          // initiator
          settlement.participateChannel.addressTo,
    
          // amount
          Utils.toBN(
            Utils.toWei(
              Number(settlement.participateChannel.amount).toString()
            )
          ),
          
          // hashed secret
          hashedSecret,

          // participant timeout
          settlement.participateChannel.timeout,
        ];

      default:
        throw Error(`Asset type ${assetType} is not supported`);
    }
  }
}
