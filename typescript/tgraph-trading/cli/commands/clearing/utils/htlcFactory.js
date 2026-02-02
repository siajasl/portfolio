import { config } from '../../../utils';
import { SWAPS } from '../../../utils/imports';

const { BitcoinHTLC, EthereumHTLC } = SWAPS;

export class HtlcFactory {
  static create(assetType, privateKey) {
    switch (assetType) {
      case 'BTC':
        return new SWAPS.BitcoinHTLC(
          privateKey,
          config.btcNetworkConfig,
        );
      case 'ETH':
        return new SWAPS.EthereumHTLC(
          { privateKey: privateKey },
          config.ethNetworkConfig,
          config.ethSmartContractAddress,
        );
      default:
        throw Error(`Asset type ${assetType} is not supported`);
    }
  }
}
