/**
 * @fileOverview A date scalar type.
 */

import * as _ from 'lodash';
import { GraphQLScalarType } from 'graphql';
import { Kind } from 'graphql/language';

/**
 * Custom date scalar type.
 */
export default new GraphQLScalarType({
    // Type resolver name - used within GQL schema.
    name: 'Date',

    // Type resolver description - used in error scenarios.
    description: 'Date custom scalar type',

    /**
     * Invoked when serializing the result to send it back to a client.
     * @param {String} value - Value AST.
     */
    serialize(value) {
        return _.isNil(value) ? null : value.getTime();
    },

    /**
     * Invoked to parse client input that was passed through variables.
     * @param {Object} value - Value to be parsed.
     */
    parseValue(value) {
        return _.isNil(value) ? null : new Date(value);
    },

    /**
     * Invoked to parse client input that was passed inline in the query.
     * @param {String} value - Value AST.
     */
    parseLiteral(ast) {
        if (_.isNil(ast.value) || ast.value.length === 0) {
            return null;
        }

        if (ast.kind === Kind.INT) {
            return new Date(parseInt(ast.value, 10));
        }

        return new Date(Date.parse(ast.value));
    },
});
