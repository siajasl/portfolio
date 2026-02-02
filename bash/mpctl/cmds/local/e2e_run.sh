#!/usr/bin/env bash

function _help() {
    echo "
    COMMAND
    ----------------------------------------------------------------
    mpctl-local-run-e2e-tests

    DESCRIPTION
    ----------------------------------------------------------------
    Runs full end to end HNSW tests.
    "
}

function _main()
{
    pushd "$(get_path_to_monorepo)/iris-mpc-upgrade-hawk" || exit
    cargo test --test e2e_genesis -- --include-ignored
    popd || exit
}

# ----------------------------------------------------------------
# ENTRY POINT
# ----------------------------------------------------------------

source "${MPCTL}"/utils/main.sh

unset _HELP

for ARGUMENT in "$@"
do
    KEY=$(echo "$ARGUMENT" | cut -f1 -d=)
    case "$KEY" in
        help) _HELP="show" ;;
        *)
    esac
done

if [ "${_HELP:-""}" = "show" ]; then
    _help
else
    _main
fi
