using System;
using System.Linq;

using static System.Console;

using HttpDoom.Records;

namespace HttpDoom.Utilities
{
    internal static class Logger
    {
        public static void Informational(string message) =>
            WriteLine($"[+] {message}");

        public static void Error(string message)=>
            WriteLine($"[!] {message}");

        public static void Warning(string message)=>
            WriteLine($"[*] {message}");

        public static void Success(string message)
        {
            Write("[+] ");

            ForegroundColor = ConsoleColor.Green;
            WriteLine(message);
            ResetColor();
        }

        public static void DisplayFlyoverResponseMessage(FlyoverResponseMessage message) =>
            WriteLine($" >  Answered {message.StatusCode}, with #{message.Headers.Count()} header(s) " +
                      $"and #{message.Cookies.Count} cookie(s)");
    }
}