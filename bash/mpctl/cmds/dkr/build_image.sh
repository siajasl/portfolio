#!/usr/bin/env bash

function _help() {
    echo "
    COMMAND
    ----------------------------------------------------------------
    mpctl-dkr-build-image

    DESCRIPTION
    ----------------------------------------------------------------
    Builds an HNSW server docker image.

    ARGS
    ----------------------------------------------------------------
    image       Kind of image to build. Options = main | genesis | e2e.

    DEFAULTS
    ----------------------------------------------------------------
    image       main
    "
}

function _main()
{
    local image=${1}

    if [ "${image}" = "main" ]; then
        _build_image "${MPCTL_DKR_FILE_HNSW_SERVER_STANDARD}" "${MPCTL_DKR_IMAGE_NAME_STANDARD}"
    elif [ "${image}" = "genesis" ]; then
        _build_image "${MPCTL_DKR_FILE_HNSW_SERVER_GENESIS}" "${MPCTL_DKR_IMAGE_NAME_GENESIS}"
    elif [ "${image}" = "e2e" ]; then
        _build_image "${MPCTL_DKR_FILE_HNSW_TESTS_E2E}" "${MPCTL_DKR_IMAGE_HNSW_TESTS_E2E}"
    else
        log_error "Invalid image label: ${image}"
    fi
}

function _build_image()
{
    local image_fname=${1}
    local image_tag=${2}

    pushd "$(get_path_to_monorepo)" || exit
    docker build \
        -f "$(get_path_to_monorepo)/${image_fname}" \
        -t "${image_tag}:latest" .
    popd || exit
}

# ----------------------------------------------------------------
# ENTRY POINT
# ----------------------------------------------------------------

source "${MPCTL}"/utils/main.sh

unset _HELP
unset _IMG

for ARGUMENT in "$@"
do
    KEY=$(echo "$ARGUMENT" | cut -f1 -d=)
    VALUE=$(echo "$ARGUMENT" | cut -f2 -d=)
    case "$KEY" in
        help) _HELP="show" ;;
        image) _IMG=${VALUE} ;;
        *)
    esac
done

if [ "${_HELP:-""}" = "show" ]; then
    _help
else
    _main "${_IMG:-"main123"}"
fi
