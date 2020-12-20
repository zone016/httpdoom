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
            WriteLine($"Answered {message.StatusCode} and has #{message.Headers.Count()} header(s)");
        }
    }
}