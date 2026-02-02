#!/usr/bin/env bash

function _help() {
    echo "
    COMMAND
    ----------------------------------------------------------------
    mpctl-dkr-build-images

    DESCRIPTION
    ----------------------------------------------------------------
    Builds Hawk server docker images.
    "
}

function _main()
{
    local images=("main" "genesis" "e2e")
    for image in "${images[@]}"; do
        source "${MPCTL}"/cmds/dkr/build_image.sh image="${image}"
    done
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
