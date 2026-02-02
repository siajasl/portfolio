/**
 * @fileOverview Represents Jumio Transaction Reference.
 */

import { mapString } from '../../utils/gql/index';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        JumioTxRef: ${mapString(obj.JumioTxRef)}
    }`;
};
