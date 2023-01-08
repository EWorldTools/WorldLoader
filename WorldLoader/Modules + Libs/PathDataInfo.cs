using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.DataClasses;
using WorldLoader.Utils;

namespace WorldLoader.ModulesLibs;

internal class PathDataInfo
{
    public static string ModPath = Internal_Utils.IsLoadedWithML ? "WLMods" : "Mods";
    public static string PluginsPath = Internal_Utils.IsLoadedWithML ? "WLPlugins" : "Plugins";
    public static string LibsPath = Internal_Utils.IsLoadedWithML ? "WLLibs" : "UserLibs";
}
