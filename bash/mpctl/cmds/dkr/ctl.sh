#!/usr/bin/env bash

function _help() {
    echo "
    COMMAND
    ----------------------------------------------------------------
    mpctl-dkr-ctl

    DESCRIPTION
    ----------------------------------------------------------------
    Exposes control functions over HNSW e2e tests docker container.

    ARGS
    ----------------------------------------------------------------
    target      Docker target: e2e | genesis | services | standard | system.
    action      Docker compose action to perform: down | run | start | stop | up.
    "
}

function _main()
{
    local target=${1}
    local action=${2}
    local mode=${3}

    case "${target}" in
    "e2e")
        case "${action}" in
            "down")
                _e2e_down
                ;;
            "run")
                _e2e_run
                ;;
            "up")
                _e2e_up
                ;;
            *)
                echo "Invalid e2e image action"
                ;;
        esac
        ;;
    "genesis")
        case "${action}" in
            "down")
                _net_down "genesis"
                ;;
            "start")
                _net_start "genesis"
                ;;
            "stop")
                _net_stop "genesis"
                ;;
            "up")
                _net_up "genesis" "${mode}"
                ;;
            *)
                echo "Invalid genesis image action"
                ;;
        esac
        ;;
    "services")
        case "${action}" in
            "down")
                _services_down
                ;;
            "reset")
                _services_reset "${mode}"
                ;;
            "up")
                _services_up "${mode}"
                ;;
            *)
                echo "Invalid services action"
                ;;
        esac
        ;;
    "standard")
        case "${action}" in
            "down")
                _net_down "standard"
                ;;
            "start")
                _net_start "standard"
                ;;
            "stop")
                _net_stop "standard"
                ;;
            "up")
                _net_up "standard" "${mode}"
                ;;
            *)
                echo "Invalid standard image action"
                ;;
        esac
        ;;
    "system")
        case "${action}" in
            "down")
                _system_down
                ;;
            "reset")
                _system_reset "${mode}"
                ;;
            "up")
                _system_up "${mode}"
                ;;
            *)
                echo "Invalid system action"
                ;;
        esac
        ;;
    *)
        echo "Invalid docker image"
        ;;
    esac
}

function _e2e_down()
{
    docker compose -f "${MPCTL_DKR_COMPOSE_HNSW_E2E}" \
        down "${MPCTL_DKR_CONTAINER_HNSW_E2E}"
    popd || exit
}

function _e2e_run()
{
    pushd "$(get_path_to_monorepo)" || exit
    docker compose -f "${MPCTL_DKR_COMPOSE_HNSW_E2E}" \
        exec "${MPCTL_DKR_CONTAINER_HNSW_E2E}" "/src/iris-mpc/scripts/run-tests-hnsw-e2e.sh"
    popd || exit
}

function _e2e_up()
{
    pushd "$(get_path_to_monorepo)" || exit
    docker compose -f "${MPCTL_DKR_COMPOSE_HNSW_E2E}" \
        up --detach "${MPCTL_DKR_CONTAINER_HNSW_E2E}"
    popd || exit
}

function _net_down()
{
    local binary=${1}
    local idx_of_node

    for idx_of_node in $(seq 0 "$((MPCTL_COUNT_OF_PARTIES - 1))")
    do
        source "${MPCTL}"/cmds/dkr/node_down.sh \
            binary="${binary}" \
            node="${idx_of_node}"
    done
}

function _net_start()
{
    local binary=${1}
    local idx_of_node

    for idx_of_node in $(seq 0 "$((MPCTL_COUNT_OF_PARTIES - 1))")
    do
        source "${MPCTL}"/cmds/dkr/node_start.sh binary="${binary}" node="${idx_of_node}"
    done
}

function _net_stop()
{
    local binary=${1}
    local idx_of_node

    for idx_of_node in $(seq 0 "$((MPCTL_COUNT_OF_PARTIES - 1))")
    do
        source "${MPCTL}"/cmds/dkr/node_stop.sh binary="${binary}" node="${idx_of_node}"
    done
}

function _net_up()
{
    local binary=${1}
    local mode=${2:-"detached"}
    local idx_of_node

    for idx_of_node in $(seq 0 "$((MPCTL_COUNT_OF_PARTIES - 1))")
    do
        source "${MPCTL}"/cmds/dkr/node_up.sh \
            binary="${binary}" \
            node="${idx_of_node}" \
            mode="${mode}"
    done
}

function _services_down()
{
    pushd "$(get_path_to_monorepo)" || exit
    docker-compose -f "${MPCTL_DKR_COMPOSE_SERVICES}" down --volumes
    popd || exit
}

function _services_reset()
{
    local mode=${1:-"detached"}

    _services_down
    _services_up ${mode}
}

function _services_up()
{
    local mode=${1:-"detached"}

    pushd "$(get_path_to_monorepo)" || exit
    if [ "$mode" == "detached" ]; then
        docker-compose -f "${MPCTL_DKR_COMPOSE_SERVICES}" up --detach
    else
        docker-compose -f "${MPCTL_DKR_COMPOSE_SERVICES}" up
    fi
    popd || exit
}

function _system_down()
{
    _net_down
    _services_down
}

function _system_reset()
{
    local mode=${1:-"detached"}

    _system_down
    _system_up ${mode}
}

function _system_up()
{
    local mode=${1:-"detached"}

    _services_up ${mode}
    _net_up ${mode}
}

# ----------------------------------------------------------------
# ENTRY POINT
# ----------------------------------------------------------------

source "${MPCTL}"/utils/main.sh

unset _ACTION
unset _IMAGE
unset _HELP
unset _MODE

for ARGUMENT in "$@"
do
    KEY=$(echo "$ARGUMENT" | cut -f1 -d=)
    VALUE=$(echo "$ARGUMENT" | cut -f2 -d=)
    case "$KEY" in
        action) _ACTION=${VALUE} ;;
        target) _TARGET=${VALUE} ;;
        help) _HELP="show" ;;
        mode) _MODE=${VALUE} ;;
        *)
    esac
done

if [ "${_HELP:-""}" = "show" ]; then
    _help
else
    _main "${_TARGET}" "${_ACTION}" "${_MODE}"
fi
