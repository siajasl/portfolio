import argparse

from stests.core import cache
from stests.core import factory
from stests.core.types.chain import AccountType
from stests.core.utils import args_validator
from stests.core.utils import cli as utils
from stests.core.utils import env
from arg_utils import get_network_node
from arg_utils import get_network_nodeset



# CLI argument parser.
ARGS = argparse.ArgumentParser("Displays a node's bonding asymmetric ECC key pair.")

# CLI argument: network name.
ARGS.add_argument(
    "--net",
    default=env.get_network_name(),
    dest="network",
    help="Network name {type}{id}, e.g. nctl1.",
    type=args_validator.validate_network,
    )

# CLI argument: node index.
ARGS.add_argument(
    "--node",
    dest="node",
    help="Node index, e.g. 1.",
    type=args_validator.validate_node_index
    )


def main(args):
    """Entry point.
    
    :param args: Parsed CLI arguments.

    """
    if args.node:
        _, node = get_network_node(args)
        nodeset = [node]
    else:
        _, nodeset = get_network_nodeset(args)

    for node in nodeset:
        utils.log_line()
        utils.log(f"VALIDATOR ACCOUNT KEYS @ NODE {node.index} ({node.address}) :")
        utils.log(f"NETWORK: {node.network} :: validator account-hash = {node.account.account_hash}")
        utils.log(f"NETWORK: {node.network} :: validator account-id = {node.account.account_key}")
        utils.log(f"NETWORK: {node.network} :: validator private-key = {node.account.private_key}")
        utils.log(f"NETWORK: {node.network} :: validator public-key = {node.account.public_key}")

    utils.log_line()


# Entry point.
if __name__ == '__main__':
    main(ARGS.parse_args())
