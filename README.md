<p align="center">
<a href="https://github.com/BizarreNULL/httpdoom/">
  <img src="./Images/logo.png" width="300" />
</a>
</p>
<h1 align="center">
  HttpDoom - <b>Flyover the horizon</b>
</h1>

<p align="center">
  Validate large HTTP-based attack surfaces in a very fast way. Heavily inspired by <a href="https://github.com/michenriksen/aquatone">Aquatone</a>.
  <br/><br/>
  <a href="http://www.wtfpl.net/txt/copying/">
    <img alt="WTFPL License" src="https://img.shields.io/github/license/BizarreNULL/shell-robot" />
  </a>
  <img alt="GitHub code size in bytes" src="https://img.shields.io/github/languages/code-size/BizarreNULL/httpdoom">
</p>



## Why?

When I utilize Aquatone to flyover some hosts, I have some performance issues by the screenshot feature, and the lack of extension capabilities - like validating front-end technologies with a plugin-like system -, also, my codebase is mainly C# and Rust, and make the maintenance of a tool wrote in another language can lead to a lot of issues.

With these ideas in mind, HttpDoom is born.



## Installing

In order to install HttpDoom, in the current release cycle, due to not have a runtime-independent build at this time (**only *devel* builds are available**), you **must have .NET5 runtime (or SDK) - AKA `dotnet` - installed in your host**, with the .NET toolchain available in your Linux or macOS (automatic installation for Windows is not supported at this time, your PR to installation script is welcome. WSL works fine):

```sh
$ ./installer.sh
```

The installer script also updates (removing the current instalation) new releases of HttpDoom.



## How this works?

The description (`--help`) of the CLI is all you need to know:

```
HttpDoom:
  HttpDoom is a tool for response-based inspection of websites across a large
  surface.
  amount of hosts for quickly gaining an overview of HTTP-based attack

Usage:
  HttpDoom [options]

Options:
  -d, --debug                             Print debugging information
  -f, --follow-redirect                   HTTP client follow any automatic
                                          redirects (default is false)
  -m, --max-redirects                     Max automatic redirect depth when is
                                          enable (default is 3)
  -s, --screenshot                        Take screenshots from the alive host
                                          with ChromeDriver (default is false)
  -r, --screenshot-resolution             Set screenshot resolution (default
                                          is 1366x768)
  -F, --capture-favicon                   Download the application favicon
  -h, --headers <headers>                 Set default headers to every request
                                          User-Agent)
                                          (default is just a random
  -t, --http-timeout <http-timeout>       Timeout in milliseconds for HTTP
                                          requests (default is 5000)
  -T, --threads <threads>                 Number of concurrent threads
                                          (default is 20)
  -o, --output-directory                  Path to save the output directory
  <output-directory>
  -p, --ports <ports>                     Set of ports to check (default is
                                          80, 443, 8080 and 8433)
  -P, --proxy <proxy>                     Proxy to use for HTTP requests
  -w, --word-list <word-list>             List of hosts to flyover against
  (REQUIRED)
  --version                               Show version information
  -?, -h, --help                          Show help and usage information
```



## But it is fast?

Let's take a look on the result of a flyover agains 5000 hosts on default HttpDoom ports (80, 443, 8080 and 8433), running in the very first working release, with 2 threads (provided by a generic Amazon EC2 instance) agains the same settings on Aquatone 1.7.0:

HttpDoom:

```
...
[+] Flyover is done! Enumerated #31128 responses in 2.49 minute(s)
[+] Got a total of #176 alive hosts!
...
```

Aquatone:

```
...
Writing session file...Time:
 - Started at  : 2020-12-20T08:27:43Z
 - Finished at : 2020-12-20T08:34:35Z
 - Duration    : 6m52s
...
```

> **Note**: The results of these tests can vary a lot based on a series of specific conditions of your host. Make the test locally and check which tool offers the best performance.



## Output

By default, we create all the necessary directories, and we also randomly choose their names (you can set this up with `-o`, in doubt see `--help`).

Within the main directory, a `general.json` file is created containing all the results in a single file (to facilitate the search or ingestion in some visual tool), which looks like this:

```json
[
    {
        "Domain": "google.com",
        "Addresses": [
            "2800:3f0:4001:81a::200e",
            "172.217.28.14"
        ],
        "Requested": "https://google.com/",
        "Port": 443,
        "Content": "\u003CHTML\u003E\u003CHEAD\u003E\u003Cmeta http-equiv=\u0022content-type\u0022 content=\u0022text/html;charset=utf-8\u0022\u003E\n\u003CTITLE\u003E301 Moved\u003C/TITLE\u003E\u003C/HEAD\u003E\u003CBODY\u003E\n\u003CH1\u003E301 Moved\u003C/H1\u003E\nThe document has moved\n\u003CA HREF=\u0022https://www.google.com/\u0022\u003Ehere\u003C/A\u003E.\r\n\u003C/BODY\u003E\u003C/HTML\u003E\r\n",
        "ScreenshotPath": "C:\\Users\\REDACTED\\AppData\\Local\\Temp\\c14obxml.kfy\\Screenshots\\0086aea9-c4d4-4bbf-89d8-728e5d2ff184.png",
        "FaviconPath": "C:\\Users\\REDACTED\\AppData\\Local\\Temp\\c14obxml.kfy\\Favicons\\172d671c-636d-443b-b5b4-30ed6e10b8aa.ico",
        "Headers": [
            {
                "Key": "Location",
                "Value": [
                    "https://www.google.com/"
                ]
            },
            {
                "Key": "Date",
                "Value": [
                    "Tue, 02 Feb 2021 15:59:46 GMT"
                ]
            },
            {
                "Key": "Cache-Control",
                "Value": [
                    "public, max-age=2592000"
                ]
            },
            {
                "Key": "Server",
                "Value": [
                    "gws"
                ]
            },
            {
                "Key": "X-XSS-Protection",
                "Value": [
                    "0"
                ]
            },
            {
                "Key": "X-Frame-Options",
                "Value": [
                    "SAMEORIGIN"
                ]
            },
            {
                "Key": "Alt-Svc",
                "Value": [
                    "h3-29=\u0022:443\u0022; ma=2592000",
                    "h3-T051=\u0022:443\u0022; ma=2592000",
                    "h3-Q050=\u0022:443\u0022; ma=2592000",
                    "h3-Q046=\u0022:443\u0022; ma=2592000",
                    "h3-Q043=\u0022:443\u0022; ma=2592000",
                    "quic=\u0022:443\u0022; ma=2592000"
                ]
            }
        ],
        "Cookies": [],
        "StatusCode": 301
    },
    // ...
]
```

A directory called *Individual Results* is also created, indexing the results individually, categorically based on the name of the URI used for the request, as well the screenshots, if you use HttpDoom with option `-s` and favicons, if the site has one, and if you use HttpDoom with option `-F`:

```
.
├── Favicons
│   ├── 31be8e61-d90b-4b40-bcef-640fb31588e7.ico
│   └── 4e097b93-12f2-4f20-9582-547cc6d20312.ico
├── Individual Results
│   ├── http:google.com:80.json
│   └── https:google.com:443.json
├── Screenshots
│   ├── 1d395ce1-b329-4379-8d9e-2868ed41e67d.png
│   └── a9f90f23-4d5c-4f13-ba3e-5d8f88aa3926.png
└── general.json
```

> **Note**: The pattern of Individual Results files is `scheme:address:port`.But `:` can be an invalid character depending on what operational system you use HttpDoom. For deeper ACK, check the documentation of `Path.GetInvalidFileNameChars()` in MSDN.

## Roadmap

The project are focused to be a really useful tool.

- [x] **0x00**: Make the satuday project work;
- [x] **0x01**: Baking the CLI options very similar to Aquatone;
- [x] **0x02**: Fix issues with large (5K+) hosts wordlists;
- [x] **0x03**: Well, this is not "threads" but work like, maybe need a better polishing;
- [x] **0x04** Screenshots because why not;
- [ ] **0x05**: Create the community-driven fingerprint engine to enumerate vulnerabilities on headers and bodies of the HTTP responses;



## Licenses

[HttpDoom](https://github.com/BizarreNULL/httpdoom) project icons made by [Freepik](www.flaticon.com/authors/freepik) from [Flaticon](https://www.flaticon.com/). The source code is licensed under [WTFPL](http://www.wtfpl.net/).