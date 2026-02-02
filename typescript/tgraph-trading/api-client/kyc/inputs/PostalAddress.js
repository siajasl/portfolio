/**
 * @fileOverview Represents a postal address.
 */

import { getPostalAddress } from '../../utils/testing';
import { mapString } from '../../utils/gql/index';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        streetLine01: ${mapString(obj.streetLine01)}
        streetLine02: ${mapString(obj.streetLine02)}
        streetLine03: ${mapString(obj.streetLine03)}
        streetLine04: ${mapString(obj.streetLine04)}
        city:         ${mapString(obj.city)}
        state:        ${mapString(obj.state)}
        county:       ${mapString(obj.county)}
        zipCode:      ${mapString(obj.zipCode)}
        country:      ${mapString(obj.country)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = () => {
    return getPostalAddress();
}
