#!/usr/bin/env bash

DESTINATION="$HOME/.httpdoom"

if ! command -v dotnet &> /dev/null; then
    echo "Please, install dotnet!"
    exit 1
fi

if [ ! -d "$DESTINATION" ]; then
    mkdir $DESTINATION
else
    dotnet tool uninstall --tool-path $DESTINATION httpdoom 2>/dev/null
fi

dotnet pack
dotnet tool install --tool-path $DESTINATION --add-source ./nupkg httpdoom

echo
echo "Add $DESTINATION to your current profile to access the binary globaly"
