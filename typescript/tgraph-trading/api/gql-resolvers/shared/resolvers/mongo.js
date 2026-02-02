/**
 * @fileOverview Sets mongo dB connection in readiness for resolution.
 */

import * as dbEngine from '../../../db/engine';
import resolvers from '../../index';
import * as resolvers_kyc from '../../kyc/index';

/**
 * Sets mongo dB connection in readiness for resolution.
 */
export default (parent, args, context, info) => {
    const qry_endpoints = Object.keys(resolvers.Query);
    const is_qry = qry_endpoints.includes(info.fieldName);

    const kyc_endpoints = Object.keys(resolvers_kyc.all);
    const is_kyc = kyc_endpoints.includes(info.fieldName);

    let connectionType = (is_qry ? 'query' : 'mutation') +
                         (is_kyc ? '_kyc' : '');

    context.dbe = dbEngine.ENGINES[connectionType];
};
