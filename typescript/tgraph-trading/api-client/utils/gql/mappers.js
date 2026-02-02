/**
 * @fileOverview GQL mapping helper functions.
 */

import * as _ from 'lodash';

/**
 * Maps target array to Gql using passed mapper function.
 * @param {Array} target - Target to be mapped to Gql.
 * @param {function} mapper - Mapping function.
 * @param {object} defaultValue - Default value when unmappable.
 * @return {object|Array|null} Mapped result.
 */
export const mapArray = (target, mapper, defaultValue) => {
    // Unbox mapping function.
    if (_.has(mapper, 'toGql')) {
        mapper = mapper.toGql;
    }

    // Map arrays.
    if (Array.isArray(target)) {
        return `[${target.map((i) => mapper(i))}]`;
    }

    return defaultValue || `[]`;
};

/**
 * Maps target to Gql using passed mapper function.
 * @param {object|Array} target - Target to be mapped to Gql.
 * @param {function} mapper - Mapping function.
 * @param {object} defaultValue - Default value when unmappable.
 * @return {object|Array|null} Mapped result.
 */
export const mapObject = (target, mapper, defaultValue) => {
    // Unbox mapping function.
    if (_.has(mapper, 'toGql')) {
        mapper = mapper.toGql;
    }

    // Map arrays.
    if (Array.isArray(target)) {
        return target.map((i) => mapper(i));
    }

    // Map objects.
    if (_.isObjectLike(target)) {
        return mapper(target);
    }

    return defaultValue || null;
};

/**
 * Maps a date value to Gql in readiness for dispatch over the wire.
 * @param {Date} val - Date value to be mapped.
 * @return {Integer|Null} Mapped result.
 */
export const mapDate = (val) => {
    if (_.isNil(val)) {
        return null;
    }

    if (_.isString(val) && val.length === 0) {
        return null;
    }

    if (_.isInteger(val)) {
        return val;
    }

    if (_.isDate(val)) {
        return val.getTime();
    }

    if (_.isString(val)) {
        return Date.parse(val);
    }
};

/**
 * Maps a string value to Gql in readiness for dispatch over the wire.
 * @param {String} val - String value to be mapped.
 * @return {String|null} Mapped result.
 */
export const mapString = (val) => {
    return val ? `"${val}"` : null;
};

/**
 * Maps a Gql response to data.
 * @param {Object} response - String value to be mapped.
 * @param {String} path - Path in response to extract.
 * @return {Object} Mapped result.
 */
export const mapResponse = (response, path) => {
    if (response && response.data && response.data[path]) {
        return response.data[path];
    }
}
