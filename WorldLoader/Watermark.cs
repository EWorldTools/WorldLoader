using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;

namespace WorldLoader;

internal class Watermark
{
    internal static void Send()
    {
        Console.Write("                                   -======-  ");
        Console.Write("   WorldClient");
        Console.Write("    -======-  ");
        Console.Write("    \n");

        Logs.Log(ConsoleColor.DarkMagenta, "             |                          .                      |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |     *   .                  .               .    |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |  .         .  .           .      .        .     |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |        o              .                   .     |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |         .              .                  .     |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |          0     .                                |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |                 .         .             ,      ,|");
        Logs.Log(ConsoleColor.DarkMagenta, "             | .           .        .                         .|");
        Logs.Log(ConsoleColor.DarkMagenta, "             |      .          ,                               |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |   .        .  ,      .                 .        |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |     .                           ,               |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |                           .                     |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |                .                        .       |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |   .         *      .        .   .       ,       |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |        .     *                         .        |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |      .                    .                     |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |          .             .                    .   |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |                      \\          .               |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |____^/\\___^--____/\\___________________/\\/\\--/    |");
        Logs.Log(ConsoleColor.DarkMagenta, "             |   /\\^   ^  ^    ^                     ^^ ^  '\\ ^|");
        Logs.Log(ConsoleColor.DarkMagenta, "             |         --            -             --  -      -|");
        Logs.Log(ConsoleColor.DarkMagenta, "             |    --  __                          ___--  ^  ^  |");
        Console.Write("                                     Made By ");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("_1254                   \n");
        Console.ResetColor();
        Logs.Log();
    }
}