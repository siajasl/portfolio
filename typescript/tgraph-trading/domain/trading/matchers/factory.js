/**
 * @fileOverview Matching aglorithm factory.
 */

import * as merchant from './merchant';
import * as otc from './otc';
import * as standard from './standard';
import { MatchingTypeEnum } from '../enums';

// Map: Algo Type <-> Matching handler.
const ALGOS = new Map([
    [MatchingTypeEnum.STANDARD, standard],
    [MatchingTypeEnum.MERCHANT, merchant],
    [MatchingTypeEnum.OTC, otc],
]);

/*
 * Returns a matching algorithm handler.
 * @param {MatchingTypeEnum} algoType - Type of matching algorithm.
 */
export default (algoType) => {
    if (ALGOS.has(algoType) === false) {
        throw new Error(`Unsupported matching algorithm: ${algoType}`)
    }

    return ALGOS.get(algoType);
}
