using System;
using System.Linq;
using System.Reflection;

namespace WorldLoader.Il2CppGen.Internal.Extensions;

internal static class AssemblyExtensions
{
    public static Type[] GetTypesSafe(this Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return ex.Types.Where(t => t != null).ToArray();
        }
    }
}
