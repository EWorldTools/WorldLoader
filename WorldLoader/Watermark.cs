using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;

namespace WorldLoader;

internal class Watermark
{
    private static string[] Heart =
{
"_░▒███████",
"░██▓▒░░▒▓██",
"██▓▒░__░▒▓██___██████",
"██▓▒░____░▓███▓__░▒▓██",
"██▓▒░___░▓██▓_____░▒▓██",
"██▓▒░_______________░▒▓██",
" ██▓▒░______________░▒▓██",
"  ██▓▒░____________░▒▓██",
"   ██▓▒░__________░▒▓██",
"    ██▓▒░________░▒▓██",
"     ██▓▒░_____░▒▓██",
"      ██▓▒░__░▒▓██",
"       █▓▒░░▒▓██",
"         ░▒▓██",
"       ░▒▓██",
"      ░▒▓██"
        };

    internal static void Send()
    {
        Console.Write("                    -======-  ");
        Console.Write("   WorldLoader");
        Console.Write("    -======-  ");
        Console.Write("    \n");

        foreach (var line in Heart)
            Logs.Log(ConsoleColor.Magenta, "         " + line);
        Console.Write("                 Made By ");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("_1254                   \n");
        Console.ResetColor();
        Logs.Log();
    }
}