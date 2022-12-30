#nullable enable
using System.Reflection;
using System;
using System.Linq;
using System.IO;

namespace WorldLoader.Il2CppGen.Internal.XrefScans;

public static class GeneratedDatabasesUtil
{
    private static string? DatabasesLocationOverride => Directory.GetCurrentDirectory() + "\\WorldLoader\\UnhollowedAsm";

    public static string GetDatabasePath(string databaseName)
    {
        return Path.Combine(
            (DatabasesLocationOverride ?? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))!,
            databaseName);
    }
}
