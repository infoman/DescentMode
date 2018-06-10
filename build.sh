#!/bin/sh
set -e

cd "$(dirname "$0")"

# Include your custom env variables
[ -e ./.env ] && . .env

rm -rvf bin obj packages

export XBUILD=${XBUILD:-/opt/mono/bin/xbuild}
export KSPPATH=${KSPPATH:-~/games/KSP-GOG/1.4.3/game}

${XBUILD} /p:Configuration=Release /tv:2.0 DescentMode.sln
mkdir -pv GameData/DescentMode/Plugins
cp -av bin/Release/*.dll GameData/DescentMode/Plugins

mkdir -pv packages
zip -r -v packages/DescentMode.zip GameData LICENSE.txt README.md
