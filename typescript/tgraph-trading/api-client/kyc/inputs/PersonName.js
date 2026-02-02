/**
 * @fileOverview Represents a person's name.
 */

import { getPersonName } from '../../utils/testing';
import { mapString } from '../../utils/gql/index';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        prefix: ${mapString(obj.prefix)}
        first:  ${mapString(obj.first)}
        middle: ${mapString(obj.middle)}
        last:   ${mapString(obj.last)}
        suffix: ${mapString(obj.suffix)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = () => {
    return getPersonName();
}
