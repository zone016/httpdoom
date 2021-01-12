#!/usr/bin/env zsh

dotnet tool uninstall --tool-path $HOME/Tools httpdoom 2>/dev/null

dotnet pack
dotnet tool install --tool-path $HOME/Tools --add-source ./nupkg httpdoom
