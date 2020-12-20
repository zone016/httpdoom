using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Diagnostics;
using System.CommandLine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.CommandLine.Invocation;
using System.Net;
using static System.Console;

using Pastel;

using HttpDoom.Records;
using HttpDoom.Utilities;

namespace HttpDoom
{
    internal static class Program
    {
        private static async Task<int> Main(string[] args)
        {
            CursorVisible = false;
            
            #region Application Banner

            var (left, top) = GetCursorPosition();
            WriteLine(string.Join(string.Empty, Banner.Hello
                .Select(s => s.ToString().Pastel("EE977F").PastelBg("FFFFC5"))));
            SetCursorPosition(left + 12, top + 2);
            Write("HttpDoom, flyover");
            SetCursorPosition(left + 12, top + 3);
            Write("to your horizon.");
            SetCursorPosition(0, top + 5);
            
            WriteLine("   v0.1".Pastel("EE977F"));
            WriteLine();
            
            #endregion
            
            #region Command Line Argument Parsing

            var commands = new RootCommand
            {
                new Option<bool>(new[] {"--debug", "-d"})
                {
                    Description = "Print debugging information"
                },
                new Option<int>(new[] {"--http-timeout", "-t"})
                {
                    Description = "Timeout in milliseconds for HTTP requests (default is 5000)"
                },
                new Option<string>(new[] {"--output-file", "-o"})
                {
                    Description = "Path to save the output file (is .JSON)"
                },
                new Option<int[]>(new[] {"--ports", "-p"})
                {
                    Description = "Set of ports to check (default is 80, 443, 8080 and 8433)"
                },
                new Option<string>(new[] {"--proxy", "-P"})
                {
                    Description = "Proxy to use for HTTP requests"
                },
                new Option<string>(new[] {"--word-list", "-w"})
                {
                    Description = "List of hosts to flyover against",
                    IsRequired = true
                }
            };

            commands.Description = "HttpDoom is a tool for response-based inspection of websites across a large " +
                                   "amount of hosts for quickly gaining an overview of HTTP-based attack surface.";
            
            #endregion
            
            commands.Handler = CommandHandler.Create<Options>(async options => await Router(options));            
            
            CursorVisible = true;
            return await commands.InvokeAsync(args);
        }

        private static async Task Router(Options options)
        {
            if (options.Debug) Logger.Informational("Validating options...");

            #region Options Assertion

            if (string.IsNullOrEmpty(options.OutputFile))
            {
                options.OutputFile = $"{Guid.NewGuid()}.json";
                
                if (options.Debug)
                {
                    Logger.Informational($"Random output file was generated {options.OutputFile}");
                }
            }
            
            if (File.Exists(options.OutputFile))
            {
                Logger.Error($"Output file {options.OutputFile} already exist!");
                Environment.Exit(-1);
            }

            if (!File.Exists(options.WordList))
            {
                Logger.Error($"Wordlist {options.WordList} don't exist!");
                Environment.Exit(-1);
            }

            if (!options.Ports.Any())
            {
                Logger.Error("You need at least one port!");
                Environment.Exit(-1);
            }

            if (options.HttpTimeout < 3000)
            {
                if (options.HttpTimeout == 666 && options.Debug)
                {
                    Logger.Error("https://www.youtube.com/watch?v=l482T0yNkeo");
                }

                Logger.Error("Invalid timeout for HTTP request. Must be >= 3000!");
                Environment.Exit(-1);
            }

            if (!string.IsNullOrEmpty(options.Proxy))
            {
                if (!options.Proxy.Contains(":"))
                {
                    Logger.Error($"{options.Proxy} is a invalid proxy string! (Correct is 127.0.0.1:1234)");
                    Environment.Exit(-1);   
                }

                var values = options.Proxy.Split(":");
                if (values.Length != 2)
                {
                    Logger.Error($"{options.Proxy} is a invalid proxy string! (Correct is 127.0.0.1:1234)");
                    Environment.Exit(-1);   
                }
            }

            #endregion

            #region Wordlist Validation

            var domains = new List<string>();
            await using var fileStream = new FileStream(options.WordList, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(fileStream);
            while (streamReader.Peek() != -1)
            {
                var target = await streamReader.ReadLineAsync();
                if (string.IsNullOrEmpty(target)) continue;

                target = target.RemoveSchema();

                while (target.EndsWith("/"))
                {
                    target = target.Remove(target.Length - 1);
                }

                if (Uri.CheckHostName(target) == UriHostNameType.Unknown)
                {
                    if (options.Debug) 
                        Logger.Error($"{target} has an invalid format to be a fully qualified domain!");
                }
                else
                {
                    domains.Add(target);
                }
            }

            domains = domains.Distinct().ToList();

            if (domains.Count == 0)
            {
                Logger.Error("Your wordlist is useless. Try a new one with actual fully qualified domains!");
                Environment.Exit(-1);
            }

            Logger.Informational($"After wordlist sanitization, the total of hosts is #{domains.Count}");
            if (options.Debug)
                Logger.Informational("Starting flyover with port(s): " +
                                     $"{string.Join(", ", options.Ports)}");

            #endregion

            #region Flyover Interactions

            var targets = new List<string>();
            domains
                .ForEach(d =>
                {
                    options.Ports.ToList()
                        .ForEach(p =>
                        {
                            targets.Add($"http://{d}:{p}");
                            targets.Add($"https://{d}:{p}");
                        });
                });

            Logger.Informational($"Added ports, the ({"possible".Pastel(Color.MediumAquamarine)}) " +
                                 $"total of requests is #{targets.Count}");
            Logger.Warning("Initializing CPU-intensive tasks (this can take a while)...");

            var stopwatch = Stopwatch.StartNew();

            var tasks = targets
                .Select(t => Trigger(t, options.Debug, options.HttpTimeout))
                .ToList();
            
            var flyoverResponseMessages = await Task.WhenAll(tasks);
            
            stopwatch.Stop();
            
            Logger.Success($"Flyover is done! Enumerated #{flyoverResponseMessages.Length} " +
                           $"responses in {stopwatch.Elapsed.TotalSeconds} second(s)");
            
            Logger.Success($"Total of valid domains is " +
                           $"#{flyoverResponseMessages.Count(f => f!=null)}");

            #endregion

            #region Result Persistance
            
            Logger.Informational($"Indexing results in {options.OutputFile}");

            flyoverResponseMessages = flyoverResponseMessages.Where(f => f != null).ToArray();
            await File.WriteAllTextAsync(options.OutputFile, 
                JsonSerializer.Serialize(flyoverResponseMessages));

            #endregion
        }

        private static async Task<FlyoverResponseMessage> Trigger(string target, bool debug, int httpTimeout)
        {
            try
            {
                if (debug) Logger.Informational($"Requesting {target}...");

                var message = await Flyover(target, httpTimeout);
                Logger.Success($"Host {target} is alive!");
                Logger.DisplayFlyoverResponseMessage(message);
                
                return message;
            }
            catch (HttpRequestException)
            {
                if (debug)
                    Logger.Warning($"Possible mismatch of protocol requesting {target}, " +
                                   "trying with SSL/TLS without port...");

                var updatedTarget = target.Replace("http://", "https://");
                if (updatedTarget.Contains(":"))
                {
                    var separator = updatedTarget.LastIndexOf(":", StringComparison.Ordinal);
                    updatedTarget = updatedTarget.Remove(separator, updatedTarget.Length - separator);
                }

                if (debug) Logger.Warning($"Requesting {updatedTarget} again...");

                try
                {
                    var message = await Flyover(updatedTarget, httpTimeout);
                    Logger.Success($"Host {updatedTarget} is alive!");
                    Logger.DisplayFlyoverResponseMessage(message);

                    return message;
                }
                catch (Exception e)
                {
                    if (debug) Logger.Error(e.InnerException == null
                        ? $"Host {target} is dead: {e.Message}"
                        : $"Host {target} is dead: {e.InnerException.Message}");
                    
                    return null;
                }
            }
            catch (Exception e)
            {
                if (debug) Logger.Error(e.InnerException == null
                    ? $"Host {target} is dead: {e.Message}"
                    : $"Host {target} is dead: {e.InnerException.Message}");

                return null;
            }
        }

        private static async Task<FlyoverResponseMessage> Flyover(string target, int timeout, string proxy = null)
        {
            using var clientHandler = new HttpClientHandler
            {
                UseCookies = false,
                MaxAutomaticRedirections = 5,
                AllowAutoRedirect = true,
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            };

            if (!string.IsNullOrEmpty(proxy))
            {
                clientHandler.Proxy = new WebProxy(proxy);
            }
            
            using var client = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromMilliseconds(timeout)
            };

            var request = new HttpRequestMessage(HttpMethod.Get, target)
            {
                Headers =
                {
                    {"User-Agent", 
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) " +
                        "Chrome/87.0.4280.88 Safari/537.36"}
                }
            };
            
            var response = await client.SendAsync(request);

            var unparsedHost = target
                .RemoveSchema()
                .Split(":");

            var domain = unparsedHost[0];
            var port = unparsedHost.Length == 1
                ? 80
                : int.Parse(unparsedHost[1]);

            return new FlyoverResponseMessage
            {
                Domain = domain,
                Port = port,
                Headers = response.Headers,
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
        }
    }
}