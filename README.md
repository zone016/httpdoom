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



## Roadmap

The project are focused to be a really useful tool.

- [x] **0x00**: Make the satuday project work;
- [x] **0x01**: Baking the CLI options very similar to Aquatone;
- [ ] **0x02**: Fix issues with large (5K+) hosts wordlists;
- [ ] **0x03**: Create the community-driven fingerprint engine to enumerate vulnerabilities on headers and bodies of the HTTP responses;



## Licenses

[HttpDoom](https://github.com/BizarreNULL/httpdoom) project icons made by [Freepik](www.flaticon.com/authors/freepik) from [Flaticon](https://www.flaticon.com/). The source code is licensed under [WTFPL](http://www.wtfpl.net/).