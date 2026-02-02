/**
 * @fileOverview A date scalar type.
 */

const validateUUID = require('uuid-validate');
import * as _ from 'lodash';
import { GraphQLScalarType } from 'graphql';
import { GraphQLError } from 'graphql/error';
import { Kind } from 'graphql/language';

export default new GraphQLScalarType({
    // Type resolver name - used within GQL schema.
    name: 'UUID',

    // Type resolver description - used in error scenarios.
    description: 'Universally unique (v4) identifiers.',

    /**
     * Invoked when serializing the result to send it back to a client.
     * @param {String} value - Value AST.
     */
    serialize(value) {
        return _.isNil(value) ? null : value;
    },

    /**
     * Invoked to parse client input that was passed through variables.
     * @param {Object} value - Value to be parsed.
     */
    parseValue(value) {
        return _.isNil(value) ? null : value;
    },

    /**
     * Invoked to parse client input that was passed inline in the query.
     * @param {String} value - Value AST.
     */
    parseLiteral(ast) {
        if (validateUUID(ast.value) === false) {
            throw new GraphQLError('UUID field values must be uuid v4 compliant.');
        }

        return ast.value;
    }
})
