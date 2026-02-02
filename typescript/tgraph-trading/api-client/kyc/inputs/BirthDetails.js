/**
 * @fileOverview Represents details regarding someone's birth.
 */

import { getBirthday, getCountry, getCity } from '../../utils/testing';
import { mapDate, mapString } from '../../utils/gql/index';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        country: ${mapString(obj.country)}
        date:    ${mapDate(obj.date)}
        place:   ${mapString(obj.place)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = () => {
    return {
        country: getCountry(),
        date:    getBirthday(),
        place:   getCity()
    };
}
