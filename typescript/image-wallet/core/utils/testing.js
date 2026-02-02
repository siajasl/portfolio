/**
 * @fileOverview Library logging utility functions.
 */
import _ from 'lodash';
import keyDeriver from '../cryptography/keyDerivation/derive';

// A set of 50 test seeds for generating test wallet keypairs.
export const SEEDS = [
    '088550004429ce1fd04e859cb05cdb7286acbe783cb0f706bf3d1269ce623a2e',
    'a2d419bf498a85f8c43c7709ce1f9250922fd45360c359fd821ff1620c09fd40',
    'b457a6fc390417a85a343d2a6a2b275f7cee6534622ead0f4bd774f18f7b2862',
    '2cb7e8e473dd1a60c70ac785c90d8cc925cd3cfbcd8a86571ccbc61115edaa60',
    'bfd10fc9d1c9e1c41df4f29bc7e147aa7ba858277f042bbd8968f514f23861e4',
    'ed94e3602c430b88030b072b5014236fc38469717111d3f1d4a32d69ce7acec6',
    'bb056acb26ad796a22473e9a17979e06382a91697d1c84ffb08f4d043099879c',
    '9758e8712c33f3ed18fc73be6423f031f91210b2aefa435c4466a9b08b4afe58',
    '08b9d82a7407c9b3a7a90179516d37ca5f9ac7e2e5d64ebea9ccff59550e598a',
    'd9de9e1531c6096744657437d3e6b360fa5ebf739e99cbac2b4c7922037c8f24',
    '81b265f59c12d801c641b806b4600795999127c4940f8ac40cf3ce01f0398e09',
    '4c40ac49ef85579ad61ca15a875d0d4328a8918b27f6e3027edfa072879a4b59',
    '3927d93b242e8eb040f0936238eaeb25d89de3650c211392d7bd29ed0d2ee258',
    '28c0b92ee387131bea6f08b3cd3865f21ec2ada3b10d541bcc53b584862d1779',
    'ad42f2e9c141c9386539072504d855d8c4af38cf9fab4df2b5af4407f05539ab',
    '2e2869fcf5196e5bb087c5c8c5f4677275625a82988ddeea404104b4f33b42dc',
    '9abf1f1559b96337d9b9c1db8edb09c20ff3fb6e2c72f7bc6139dcb4d6b3b6e7',
    'f4fc760b2fc03ef1a60090105e1f225a2b86dfcd3da402425a42ce41dd5c2ae7',
    '7a59bd30271bfcbed0cd5c5474b0f6ed1b7351efaf82eec50436bda025d94cae',
    '51e7cf13aa7a793357f7309120b0b9be1985ee0f49b737b466fc7a69d70ec4fe',
    'a6b54116e39869df7b665a006311047e63c47f8d22749dc6efa5df598c0971db',
    '40ec9f1e82223a4be64d959a7d7eafff132058f3dd03fed5428fdb8ce702a83c',
    'b8d2b38106f1b519f6a0e571e850acb5350e4a7cf40ad8472e6020099e664a0d',
    '19581aeb783df28bf0dade02f83d418ef7d082d5e31519e46ae0f59f67f63081',
    '6921c907d0906bf1d122b062c81e5b672b531876422d4c5db5eac81312ed70c4',
    '2506e1c7eaf5ea3b6b45e8fd978a2e00efe05084471f2b6fad4ee15525380ef4',
    '49bd39d5b5fbf63aa2e5b52227d6ff777b248eca28a8736bbff28b6b70d49a61',
    '6caae740729593c1e3cda2056c02d439ded79d83eb538e39c1c35fbb094cd14c',
    '957fa07fc59d8a0b08a4eddedca3a28e0fadd77e95a1e0b6ab97b62c6a724601',
    '014cb57255c1ee168be99e83bf99b24af7e7063e7cb544d38abdcd27c1445fc4',
    '92aeb77f898343f68aba3e22c4de9f2462922cb41f315c1709709bc4f50b5fc1',
    '1bc95d7ee3eddc477378f92681e07cb94f02d9452abd6d592e6ed8a492f09e70',
    'bbe824bf77108a3c37b76850194507364846d0a1463e1362a4e4ac597faaa886',
    'fa745d1024fc7f9b6dc24a2c68623d6273b4d354bee4a8a2158abfdc94b539d6',
    '76378ff7cadb1ba69787b72c43d6fa890740c7869b6087b2bec75333f7e771e1',
    '111d590b60140ed4900e651a3ff9a236a28f7adf3fdedf85c3d1e00fe1638611',
    '6fd4e9e1cdda2a20e3affd97f49393375bb37945577b74b73278415f15f223bd',
    '6867835893785945f53a691b43a19b1cda1c1ffa8aadf86b385ed6f8465617fe',
    'd667fbfc292ac52635c1ec81da7001fbc6da6be2637237803dc76f6764168746',
    'de4f51c0888a3e43a4abf2dd2412dc6d7ee15cdf835d691c35581686b9ad982e',
    'eb8225bcafecc3b0d70fcb1868de0114e7b41188ae4d72f060c5dce41b6b44f7',
    'fe1e333709b0a93f14dea5e9a01223c135ee8e9e4ee1f38f3079704b3237c843',
    'a38157a2e5be2539871b1f452975b595e95784df73a1ab2a0439504375754062',
    'eabad1849b6733bbac7e415ff7619d21a9d630be0ff462ef88f55e0e33d5b739',
    '84fd1d3dcc75e3dbb5d2db5d92542e66450097f667e006baad1c7cb1123401b3',
    'e814aef8c0c95ee66ae5084e65f6304abbbf40644593961076defce46d056282',
    '2b5993e3dd5072062b73d21d2e06d0d43d230e30949a8386ca7ad015b48bdebe',
    'e4109990d0faf8a833101546390bbb8c73b620897fcf3b537bb03ef686ae1f74',
    '3a8246bbe6687a6678c0373c61d056c8183bfd868e2fe7047aba578b2002ad8e',
    '92f598a85be8fd8672afce99783e6a0e401ecfdb5b1a42a1d776c690bbdd9849',
];

// Seeds reserved for individual customers.
export const SEEDS_FOR_INDIVIDUAL_CUSTOMERS = SEEDS.slice(0, 30);

// Seeds reserved for corporate customers.
export const SEEDS_FOR_CORPORATE_CUSTOMERS = SEEDS.slice(30, 40);

// Seeds reserved for market makers.
export const SEEDS_FOR_MARKET_MAKERS = SEEDS.slice(40);

/**
 * Returns an access file key pair ready for signing.
 */
export const getTestKeyPair = (seed, coinSymbol, accountIndex) => {
    seed = seed || SEEDS[_.random(SEEDS.length)];
    coinSymbol = coinSymbol || 'AF';
    accountIndex = accountIndex || 0;

    const kp = keyDeriver(seed, coinSymbol, accountIndex);
    kp.pbk = kp.publicKey;
    kp.pvk = kp.privateKey;

    return kp;
};

/**
 * Returns an access file key pair ready for signing.
 */
export const getTestKeyPairs = (seeds, coinSymbol, accountIndex) => {
    seeds = seeds || SEEDS;
    coinSymbol = coinSymbol || 'AF';
    accountIndex = accountIndex || 0;

    return _.map(seeds, (s) => getTestKeyPair(s, coinSymbol, accountIndex));
};

/**
 * Returns access file key pair ready for signing by an individual customer.
 */
export const getTestKeyPairForIndividualCustomer = () => {
    const seeds  = SEEDS_FOR_INDIVIDUAL_CUSTOMERS;
    const seed = seeds[_.random(seeds.length)];

    return getTestKeyPair(seed);
};

/**
 * Returns access file key pairs ready for signing by individual customers.
 */
export const getTestKeyPairsForIndividualCustomers = () => {
    return getTestKeyPairs(SEEDS_FOR_INDIVIDUAL_CUSTOMERS);
};

/**
 * Returns access file key pair ready for signing by a corporate customer.
 */
export const getTestKeyPairForCorporateCustomers = () => {
    const seeds  = SEEDS_FOR_CORPORATE_CUSTOMERS;
    const seed = seeds[_.random(seeds.length)];

    return getTestKeyPair(seed);
};

/**
 * Returns access file key pairs ready for signing by corporate customers.
 */
export const getTestKeyPairsForCorporateCustomers = () => {
    return getTestKeyPairs(SEEDS_FOR_CORPORATE_CUSTOMERS);
};

/**
 * Returns access file key pair ready for signing by a corporate customer.
 */
export const getTestKeyPairForMarketMaker = () => {
    const seeds  = SEEDS_FOR_MARKET_MAKERS;
    const seed = seeds[_.random(seeds.length)];

    return getTestKeyPair(seed);
};

/**
 * Returns access file key pairs ready for signing by market makers.
 */
export const getTestKeyPairsForMarketMakers = () => {
    return getTestKeyPairs(SEEDS_FOR_MARKET_MAKERS);
};
