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



## How this works?

The description (`--help`) of the CLI is all you need to know:

![Output of `--help`](./Images/example.png)



## But it is fast?

Let's take a look on the result of a flyover agains 5000 hosts on default HttpDoom ports (80, 443, 8080 and 8433) with 2 threads (provided by a generic Amazon EC2 instance) agains the same settings on Aquatone 1.7.0:

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

HttpDoom generate a unique file as output: A single `.json`. The content looks like:

```json
[
    {
        "domain": "youtube.com",
        "host_addresses": [
            "2800:3f0:4001:802::200e",
            "172.217.29.14"
        ],
        "requested_uri": "https://youtube.com/",
        "port": 443,
        "content": "TRUNCATED",
        "headers": [
          // ...
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
          // ...
        ],
        "cookies": [
            {
                "Comment": "",
                "CommentUri": null,
                "HttpOnly": false,
                "Discard": false,
                "Domain": ".youtube.com",
                "Expired": false,
                "Expires": "2037-12-31T21:00:00-03:00",
                "Name": "CONSENT",
                "Path": "/",
                "Port": "",
                "Secure": false,
                "TimeStamp": "2020-12-20T05:42:27.994275-03:00",
                "Value": "WP.28e7b4",
                "Version": 0
            }
        ],
        "status_code": 302
    }
]
```



## Roadmap

The project are focused to be a really useful tool.

- [x] **0x00**: Make the satuday project work;
- [x] **0x01**: Baking the CLI options very similar to Aquatone;
- [x] **0x02**: Fix issues with large (5K+) hosts wordlists;
- [ ] **0x03**: Well, this is not "threads" but work like, maybe need a better polishing;
- [ ] **0x03**: Create the community-driven fingerprint engine to enumerate vulnerabilities on headers and bodies of the HTTP responses;



## Licenses

[HttpDoom](https://github.com/BizarreNULL/httpdoom) project icons made by [Freepik](www.flaticon.com/authors/freepik) from [Flaticon](https://www.flaticon.com/). The source code is licensed under [WTFPL](http://www.wtfpl.net/).