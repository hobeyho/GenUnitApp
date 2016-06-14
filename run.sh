#!/usr/bin/env bash

set -eu
set -o pipefail

cd `dirname $0`

OS=${OS:-"unknown"}

function run() {
  if [[ "$OS" != "Windows_NT" ]]
  then
    mono "$@"
  else
    "$@"
  fi
}

run bin/Informedica.GenUnit.App.exe "$@"