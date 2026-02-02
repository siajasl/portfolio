/**
 * @fileOverview Represents a document such as a utilty bill.
 */

import { getCountry } from '../../utils/testing';
import { mapDate, mapString } from '../../utils/gql/index';
import DocumentEncodingType from './DocumentEncodingType';

/**
 * Encodes instance as GQL input.
 */
export const toGql = (obj) => {
    return `{
        content:       ${mapString(obj.content)}
        documentType:  ${mapString(obj.documentType)}
        encoding:      ${obj.encoding}
        jurisdiction:  ${mapString(obj.jurisdiction)}
        validFromDate: ${mapDate(obj.validFromDate)}
        validToDate:   ${mapDate(obj.validToDate)}
    }`;
};

/**
 * Returns an instance for testing purposes.
 */
export const create = (docType) => {
    return {
        content:       'a-base64-encoded-binary-blob',
        documentType:  docType || "A test document",
        encoding:      DocumentEncodingType.Base64,
        jurisdiction:  getCountry(),
        validFromDate: Date.now(),
        validToDate:   Date.now(),
    };
}
