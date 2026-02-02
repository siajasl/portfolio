/**
 * @fileOverview Exposes an event emitter for intra-module communications.
 */

import { EventEmitter as BaseEventEmitter } from 'eventemitter3';

/**
 * An event emitter for intra-module communications.
 * @constructor
 * @param {string} scope - Scope within which events are bing emitted.
 */
export default class EventEmitter extends BaseEventEmitter {
    constructor(scope) {
        super();
        this.scope = scope;
    }

    /*
     * Delegate to base class event emitting.
     */
    emit(e) {
        // Emit confirm event - to allow cancellations.
        const confirmOpts = {
            cancel: false
        };
        super.emit(`${e}:confirm`, confirmOpts);
        if (confirmOpts.cancel === true) {
            return;
        }

        // Emit before event.
        super.emit(`${e}:before`);

        // Emit execution event.
        super.emit(...arguments);

        // Emit after event.
        super.emit(`${e}:after`);
    }
}
