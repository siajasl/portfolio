/**
 * @fileOverview Initialises file system in readiness for I/O.
 */

import * as fs from 'fs';
import { DIR_ALL } from './constants';

/**
 * Initialises file system in readiness for I/O.
 */
export default async () => {
    for (const dir of DIR_ALL) {
        if (fs.existsSync(dir) === false) {
            fs.mkdirSync(dir, { recursive: true });
        }
    }
}
