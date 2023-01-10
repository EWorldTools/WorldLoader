using Il2CppInterop.Generator.Contexts;

namespace Il2CppInterop.Generator.Passes;

internal static class Pass15GenerateMemberContexts
{
    internal static bool HasObfuscatedMethods;

    internal static void DoPass(RewriteGlobalContext context)
    {
        foreach (var assemblyContext in context.Assemblies)
            foreach (var typeContext in assemblyContext.Types)
                typeContext.AddMembers();
    }
}
