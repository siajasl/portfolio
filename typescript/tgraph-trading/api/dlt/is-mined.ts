import { ethWalletService } from '@trinkler/accessfile-tx';

export const isEthTxMined = async (txId: string) => {
  try {
    const tx = await ethWalletService.getTransaction(txId);
    return tx;
  } catch (error) {
    return null;
  }
}
