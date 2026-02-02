import EventEmitter from '../utils/events';

// Instantiate intra-package event emitter.
export const eventEmitter = new EventEmitter('clearing');
