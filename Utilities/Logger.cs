using System.Linq;
using System.Drawing;

using static System.Console;
using static System.Environment;

using Pastel;

using HttpDoom.Records;

namespace HttpDoom.Utilities
{
    internal static class Logger
    {
        public static void Informational(string message)
        {
            Write("[+]".Pastel(Color.MediumAquamarine));
            Write(" ");
            WriteLine(message);
        }

        public static void Error(string message)
        {
            Write("[!]".Pastel("EE977F").PastelBg(Color.Black));
            Write(" ");
            WriteLine(message.Pastel("EE977F").PastelBg(Color.Black));
        }

        public static void Warning(string message)
        {
            Write("[*]".Pastel(Color.Orange).PastelBg(Color.Black));
            Write(" ");
            WriteLine(message.Pastel(Color.Orange).PastelBg(Color.Black));
        }

        public static void Success(string message)
        {
            Write("[+]".Pastel(Color.MediumSpringGreen).PastelBg(Color.Black));
            Write(" ");
            WriteLine(message.Pastel(Color.MediumSpringGreen).PastelBg(Color.Black));
        }

        public static void DisplayFlyoverResponseMessage(FlyoverResponseMessage message)
        {
            Write(" > ".Pastel(Color.MediumAquamarine));
            Write(" ");
            WriteLine($"Has #{message.Headers.Count()} header(s)");
            Write(" > ".Pastel(Color.MediumAquamarine));
            Write(" ");
            WriteLine($"Status code is {message.StatusCode}");
            if (string.IsNullOrEmpty(message.Content)) return;

            Write(" > ".Pastel(Color.MediumAquamarine));
            Write(" ");
            WriteLine($"Response has a length of #{message.Content.Length} char(s)");
        }
    }
}