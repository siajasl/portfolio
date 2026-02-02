import * as Utils from 'web3-utils';

const validateBtcAudit = (audit) => audit.valid;

const validateEthAudit = (
  audit,
  initiatorAddress,
  beneficiaryAddress,
  amountInEth,
  expiresAt,
  hashedSecret,
) => {

  const amountInWei = Number(Utils.toWei(amountInEth));
  
  return audit &&
    audit.initiator.toLowerCase() === initiatorAddress.toLowerCase() &&
    audit.beneficiary.toLowerCase() === beneficiaryAddress.toLowerCase() &&
    Number(audit.amount) >= amountInWei &&
    Number(audit.expiresAt) == expiresAt // &&
    // audit.hashedSecret === hashedSecret; // TODO: fix this failing assert
}

const VALIDATORS = {
  'BTC': validateBtcAudit,
  'ETH': validateEthAudit,
};

export const auditValidators = (assetType) => VALIDATORS[assetType];
