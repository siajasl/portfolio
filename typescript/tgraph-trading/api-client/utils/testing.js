/**
 * @fileOverview Testing related utility functions.
 */

import { Chance } from 'chance';
import faker from 'faker';

// Export chance library singleton used to create test domain model instances.
export const chance = Chance();

/**
 * Returns a test birthday.
 */
export const getBirthday = () => {
    return chance.birthday();
}

/**
 * Returns a test boolean value.
 */
export const getBoolean = () => {
    return chance.bool();
}

/**
 * Returns a test string representing a city.
 */
export const getCity = () => {
    return chance.city();
}

/**
 * Returns a test country code.
 */
export const getCountry = () => {
    return chance.country();
}

/**
 * Returns a test company name.
 */
export const getCompanyName = () => {
    return chance.company();
}

/**
 * Returns a test date.
 */
export const getDate = (year) => {
    year = year | '2019';
    return chance.date({year})
}

/**
 * Returns a test array element.
 */
export const getElement = (obj) => {
    return faker.random.objectElement(obj);
}

/**
 * Returns a test email address.
 */
export const getEmail = () => {
    return chance.email({domain: "trinkler.software"});
}

/**
 * Returns a test value from an enumeration.
 */
export const getEnum = (enumDefinition) => {
    return faker.random.objectElement(enumDefinition);
}

/**
 * Returns a test hash, default length = 32.
 */
export const getHash = (length=32) => {
    return chance.hash({length});
}

/**
 * Returns a test natural number.
 */
export const getNumber = (min=1500, max=1500000) => {
    return chance.natural({min, max});
}

/**
 * Returns a test person's name.
 */
export const getPersonName = () => {
    return {
        prefix: chance.prefix(),
        first:  chance.first(),
        middle: chance.first(),
        last:   chance.last(),
        suffix: chance.suffix(),
    };
}

/**
 * Returns a test phone number.
 */
export const getPhoneNumber = () => {
    return `+1${chance.phone({ country: 'us', formatted: false })}`;
}

/**
 * Returns a test profession.
 */
export const getProfession = () => {
    return chance.profession();
}

/**
 * Returns a test postal address.
 */
export const getPostalAddress = () => {
    return {
        streetLine01: chance.address(),
        streetLine02: null,
        streetLine03: null,
        streetLine04: null,
        city:         chance.city(),
        state:        chance.state(),
        county:       chance.province(),
        zipCode:      chance.zip(),
        country:      chance.country(),
    };
}

/**
 * Returns a test UUID.
 */
export const getUUID = () => {
    return faker.random.uuid();
}
