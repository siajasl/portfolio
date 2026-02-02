#!/usr/bin/env bash

function _help() {
    echo "
    COMMAND
    ----------------------------------------------------------------
    mpctl-infra-node-activate-env

    DESCRIPTION
    ----------------------------------------------------------------
    Activates a node's environment.

    ARGS
    ----------------------------------------------------------------
    batchsize   Size of indexation batches.
    node        Ordinal identifier of node.

    DEFAULTS
    ----------------------------------------------------------------
    batchsize   64
    "
}

function _main()
{
    local idx_of_node=${1}
    local size_of_batch=${2}

    # Set automatic variable exportation.
    set -a

    # Standard evars.
    source "$(get_path_to_monorepo)/.test.env"
    source "$(get_path_to_monorepo)/.test.hawk${idx_of_node}.env"

    # Rust evars.
    export RUST_BACKTRACE=full
    export RUST_LOG=info
    export RUST_MIN_STACK=104857600

    # AWS evars.
    export AWS_ENDPOINT_URL="http://127.0.0.1:4566"
    export SMPC__AWS__ENDPOINT="http://127.0.0.1:4566"

    # Application evars.
    export SMPC__ANON_STATS_DATABASE__URL="postgres://postgres:postgres@localhost:5432/SMPC_dev_${idx_of_node}"
    export SMPC__CPU_DATABASE__URL="postgres://postgres:postgres@localhost:5432/SMPC_dev_${idx_of_node}"
    export SMPC__DATABASE__URL="postgres://postgres:postgres@localhost:5432/SMPC_dev_${idx_of_node}"
    # export SMPC__HNSW_SCHEMA_NAME_SUFFIX=_hnsw
    export SMPC__MAX_BATCH_SIZE=${size_of_batch}
    export SMPC__PARTY_ID="${idx_of_node}"
    export SMPC__SERVER_COORDINATION__PARTY_ID="${idx_of_node}"
    # export SMPC__SERVER_COORDINATION__NODE_HOSTNAMES='["127.0.0.1","127.0.0.1","127.0.0.1"]'
    export SMPC__SERVER_COORDINATION__NODE_HOSTNAMES='["0.0.0.0","0.0.0.0","0.0.0.0"]'

    # Unset automatic variable exportation.
    set +a
}

# ----------------------------------------------------------------
# ENTRY POINT
# ----------------------------------------------------------------

source "${MPCTL}"/utils/main.sh

unset _HELP
unset _IDX_OF_NODE
unset _SIZE_OF_BATCH

for ARGUMENT in "$@"
do
    KEY=$(echo "$ARGUMENT" | cut -f1 -d=)
    VALUE=$(echo "$ARGUMENT" | cut -f2 -d=)
    case "$KEY" in
        batchsize) _SIZE_OF_BATCH=${VALUE} ;;
        help) _HELP="show" ;;
        node) _IDX_OF_NODE=${VALUE} ;;
        *)
    esac
done

if [ "${_HELP:-""}" = "show" ]; then
    _help
else
    _main \
        "${_IDX_OF_NODE}" \
        "${_SIZE_OF_BATCH:-64}"
fi
