/**
 * @fileOverview Hashed locked timeout settings.
 */

/**
 * HTLC timeout settings.
 * @constructor
 */
export class HtlcTimeouts {
    constructor ({
        initiate,
        participate,
        participateEffective
    }) {
        // Number of seconds before swap channel one is considered to have timed out.
        this.initiate = initiate || 21600;

        // Number of seconds before swap channel two is considered to have timed out.
        this.participate = participate || 10800;

        // Number of seconds before swap channel two is effectively considered to have timed out.
        this.participateEffective = participateEffective || 7200;
    }
}
