using Il2CppGen.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;

namespace WorldLoader.DataClasses
{
    public class UnityAppInfo
    {
        internal static string UnityVer;

        internal UnityAppInfo()
        {
            return;
           

            "    -======================- Game Info -======================-".WriteLineToConsole(ConsoleColor.DarkBlue);
            $"EngineVersion : {UnityVer}".WriteLineToConsole(ConsoleColor.Green);
            $"GameVersion : {version}".WriteLineToConsole(ConsoleColor.DarkRed);
            $"Name : {productName}".WriteLineToConsole(ConsoleColor.DarkGreen);
            $"DataPath : {dataPath}".WriteLineToConsole(ConsoleColor.DarkGray);
            $"StreamingAssetsPath : {streamingAssetsPath}".WriteLineToConsole(ConsoleColor.DarkYellow);
            "    -======================- Game Info -======================-".WriteLineToConsole(ConsoleColor.DarkBlue);


        }

        public override string ToString()
        {
            return "    -======================- Game Info -======================-\n" +
            $"EngineVersion : {UnityVer}\n" +
            $"GameVersion : {version}\n" +
            $"Name : {productName}\n" +
            $"DataPath : {dataPath}\n" +
            $"StreamingAssetsPath : {streamingAssetsPath}\n" +
            "    -======================- Game Info -======================-";
        }

        internal string productName
        {
            get {
                var clazz = IL2CPP.GetIl2CppClass("UnityEngine.CoreModule.dll", "UnityEngine", "Application");
                var fieldIntPrt = IL2CPP.GetIl2CppField(clazz, nameof(productName));
                return IL2CPP.Il2CppStringToManaged(fieldIntPrt);
            }
        }

        internal string version
        {
            get {
                var clazz = IL2CPP.GetIl2CppClass("UnityEngine.CoreModule.dll", "UnityEngine", "Application");
                var fieldIntPrt = IL2CPP.GetIl2CppField(clazz, nameof(version));
                return IL2CPP.Il2CppStringToManaged(fieldIntPrt);
            }
        }

        internal string dataPath
        {
            get {
                var clazz = IL2CPP.GetIl2CppClass("UnityEngine.CoreModule.dll", "UnityEngine", "Application");
                var fieldIntPrt = IL2CPP.GetIl2CppField(clazz, nameof(dataPath));
                return IL2CPP.Il2CppStringToManaged(fieldIntPrt);
            }
        }

        internal string streamingAssetsPath
        {
            get {
                var clazz = IL2CPP.GetIl2CppClass("UnityEngine.CoreModule.dll", "UnityEngine", "Application");
                var fieldIntPrt = IL2CPP.GetIl2CppField(clazz, nameof(streamingAssetsPath));
                return IL2CPP.Il2CppStringToManaged(fieldIntPrt);
            }
        }
    }
}
