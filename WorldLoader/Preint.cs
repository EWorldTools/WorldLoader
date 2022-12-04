using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;
using System.Runtime.InteropServices;
using System.Net;
using WorldLoader.OtherLibraries;

public class C {
    public static WorldConfig<Values> L;
}

public class Values
{
    public bool Debug { get; set; } = true;
    public bool HollowerPassAllNames { get; set; } = false;
    public bool UnhollowerLogTrace { get; set; } = false;
    public string GameAssemblyPath { get; set; } = "";
    public string GameAssemblyHash { get; set; } = "";
    public long DeObbMapHash { get; set; } = 0;
    public string ManagedPath { get; set; } = "";
    public string DeobfuscationRegex { get; set; } = "";
    public string UnityVersion { get; set; } = "0.0.0.0";
    public string DumperVersion { get; set; } = "0.0.0.0";
}

namespace WorldLoader
{
    internal class Preint
    {
        private static readonly string[] Folders = {
            "WorldLoader",
            "Mods",
            "Reversed",
        };

        public const int STD_INPUT_HANDLE = -10;
        public const int STD_OUTPUT_HANDLE = -11;
        public const int STD_ERROR_HANDLE = -12;

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", EntryPoint = "SetStdHandle", SetLastError = true)]
        public static extern bool SetStdHandle(int nStdHandle, IntPtr hHandle);

        public static void Start()
        {
            SetStdHandle(STD_OUTPUT_HANDLE, IntPtr.Zero);
            //SetStdHandle(STD_ERROR_HANDLE, IntPtr.Zero);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            foreach (var folder in Folders)
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                    Logs.Log("Made Missing Directory! ["+folder+"]", "Prinit");
                }
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "WorldLoader\\Dependencies"));
            if (!File.Exists($"{Directory.GetCurrentDirectory()}\\WorldLoader\\Dependencies\\discord-rpc.dll"))
            {
                var wc = new WebClient();
                wc.DownloadFile("https://raw.githubusercontent.com/Hacker1254/WorldClient-Files/main/discord-rpc.dll", $"{Directory.GetCurrentDirectory()}\\WorldLoader\\Dependencies\\discord-rpc.dll");
                Logs.Log("Installed Discord RPC Dll!", "Discord RPC", ConsoleColor.White, ConsoleColor.Cyan);
            }
            Discord.Discord.Init();
            C.L = new(Environment.CurrentDirectory + "\\WorldLoader\\Config.json");
        }
    }
}