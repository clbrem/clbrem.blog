#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PORT="${1:-8000}"
URL="http://localhost:${PORT}"
PID_FILE="${SCRIPT_DIR}/.revealjs-serve.pid"
LOG_FILE="${SCRIPT_DIR}/.revealjs-serve.log"
WATCH_FILE="${SCRIPT_DIR}/index.html"

port_in_use=false
if command -v lsof >/dev/null 2>&1; then
  if lsof -iTCP:"${PORT}" -sTCP:LISTEN >/dev/null 2>&1; then
    port_in_use=true
  fi
fi

if [[ "$port_in_use" == "false" ]]; then
  nohup npx --yes live-server "${SCRIPT_DIR}" --port="${PORT}" --host=0.0.0.0 --no-browser --watch="${WATCH_FILE}" >"${LOG_FILE}" 2>&1 &
  SERVER_PID="$!"
  echo "$SERVER_PID" >"${PID_FILE}"
  sleep 1
  echo "Started live-reload server PID ${SERVER_PID} on ${URL}"
else
  echo "A server is already listening on port ${PORT}; reusing existing process."
fi

if command -v xdg-open >/dev/null 2>&1; then
  xdg-open "${URL}" >/dev/null 2>&1 || true
elif command -v open >/dev/null 2>&1; then
  open "${URL}" >/dev/null 2>&1 || true
else
  echo "No browser opener found. Open ${URL} manually."
fi

echo "Preview URL: ${URL}"
if [[ -f "${PID_FILE}" ]]; then
  echo "To stop this server: kill $(cat "${PID_FILE}")"
else
  echo "To stop server on this port, stop the process bound to ${PORT}."
fi
