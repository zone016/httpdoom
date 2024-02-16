# Installing

You must have `dotnet` SDK at version 6+:

```sh
dotnet pack -c Release -o nupkg
dotnet tool install --global --add-source .\nupkg\ httpdoom.console
```

Uninstalling:

```sh
dotnet tool uninstall -g httpdoom.console
```



## How this works?

The description (`--help`) of the CLI is all you need to know:

```
HttpDoom.Console
  Minimalist (and VERY fast) HTTP-based attack surface analysis tool

Usage:
  HttpDoom.Console [options]

Options:
  -H, --headers <headers>                               Headers to be used in every request
  -p, --ports <ports>                                   Default ports for testing (default is 80, 443).
  -w, --wordlist <wordlist> (REQUIRED)                  Path to the wordlist with targets to flyover against
  -o, --output <output>                                 Directory to save all the enumerated information
  -sR, --screenshot-resolution <screenshot-resolution>  If -S, the resolution of the screenshot (default is 1920x1080)
  -a, --allow-automatic-redirect                        If HttpDoom will follow HTTP redirects (default is true)
  -S, --screenshot                                      If HttpDoom will take screenshots from the website (default is false)
  -v, --verbose                                         If HttpDoom will print errors, only useful for debugging (default is false)
  -s, --show-details                                    If HttpDoom will print with details in stdout all the information got (default is false)
  -i, --ignore-tls                                      If HttpDoom will ignore invalid TLS for HTTPS requests (default is true)
  -r, --resolve                                         Resolve the domain enumerating the nameservers (default is false)
  -aL, --max-allowed-redirect <max-allowed-redirect>    Set the limit of automatic redirects if -a is true (default is 4)
  -T, --timeout <timeout>                               Set the timeout for HTTP responses (default is 4000)
  -t, --threads <threads>                               Set how many threads will HttpDoom utilize in runtime (default is 4)
  --version                                             Show version information
  -?, -h, --help                                        Show help and usage information
```

## Licenses

[HttpDoom](https://github.com/zone016/httpdoom) project icons made by [Freepik](www.flaticon.com/authors/freepik) from [Flaticon](https://www.flaticon.com/). The source code is licensed under [WTFPL](http://www.wtfpl.net/).
