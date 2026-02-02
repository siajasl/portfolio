import { default as mongoose } from 'mongoose';

/**
 * Returns a schema specialised for recording state transitions.
 *
 * @param {Object} stateEnum - enumeration of allowed states.
 * @param {String} dbCollectionName - name of db collection within mongo db.
 */
export const createStateHistorySchema = (stateEnum, dbCollectionName) => {
    return new mongoose.Schema({
        state: {
            type: String,
            required: true,
            enum: Object.keys(stateEnum)
        },
        timestamp: {
            default: () => Date.now(),
            type: Date,
            required: true
        }
    }, {
        _id: false,
        collection: dbCollectionName
    });
}
