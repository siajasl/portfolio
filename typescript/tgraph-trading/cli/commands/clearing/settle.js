/**
 * @fileOverview Pulls & renders settlement information.
 */

import { API } from '../../utils/imports';
import { cache, executor, parsing } from '../../utils/index';
import { renderMessage } from '../../renderers/utils';
import doInitiate from './initiate/doInitiateSettlement';
import doParticipate from './participate/doParticipateSettlement';

/**
 * Command execution entry point.
 */
const execute = async ({ settlementId: settlementID }, opts) => {
    // Pull.
    const settlement = await API.clearing.getSettlement({
        customerID: cache.getCustomerID(),
        settlementID,
    });

    // Validate.
    validate(settlement);

    // Execute.
    if (settlement.counterPartyOneID === cache.getCustomerID()) {
        await doInitiate(settlement, settlement.initiateChannel);
    } else {
        await doParticipate(settlement, settlement.participateChannel);
    }

    // Render.
    if (settlement.counterPartyOneID === cache.getCustomerID()) {
        await renderMessage(`settlement channel one established: ${settlementID}`)
    } else {
        await renderMessage(`settlement channel two established: ${settlementID}`)
    }
};

/**
 * Validates settlement pulled from API.
 */
const validate = (settlement) => {
    if (settlement === null) {
        throw new Error('Either settlement does not exist or you are not a settlement counter-party');
    }

    if (settlement.counterPartyOneID === cache.getCustomerID()) {
        if (settlement.state !== 'InitiateSetupAwaiting') {
            throw new Error('settlement initiation either elapsed or executed');
        }
    }

    if (settlement.counterPartyTwoID === cache.getCustomerID()) {
        if (settlement.state !== 'ParticipateSetupAwaiting') {
            throw new Error('settlement participation either elapsed or executed');
        }
    }
};

// Export command.
export default (program) => {
    program
        .command('settle', 'Either initiate or participate within a settlement')
        .argument('<settlementId>', 'Settlement ID', parsing.parseUUID)
        .action((args, opts) => executor(execute, args, opts));
}
