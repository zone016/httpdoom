#!/usr/bin/env bash

if ! command -v dotnet &> /dev/null; then
    echo "Please, install dotnet!!"
    exit 1
fi

dotnet tool uninstall --tool-path $HOME/Tools httpdoom 2>/dev/null

dotnet pack
dotnet tool install --tool-path $HOME/Tools --add-source ./nupkg httpdoom
