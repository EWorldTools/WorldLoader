﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldLoader.DataClasses;

public class InternalInfo
{
    public static string Name { get; } = "WorldLoader";
    public static double Version { get; } = 0.8;
    public static string Description { get; } = "A IL2Cpp Mod Loader, Allowing u to Load C# Managed Dlls";
    public static string Author { get; } = "_1254";
    public static string Thanks { get; } = "Cpp2IL [https://github.com/SamboyCoding/Cpp2IL] is licensed under the MIT License. See LICENSE for the full License.";
    public static UnityVer EngineVersion { get; internal set; }


}
