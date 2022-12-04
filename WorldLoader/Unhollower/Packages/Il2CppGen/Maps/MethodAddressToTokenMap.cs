#nullable enable

using System.Reflection;

namespace WorldLoader.Il2CppGen.Internal.Maps;

public class MethodAddressToTokenMap : MethodAddressToTokenMapBase<Assembly, MethodBase>
{
    public MethodAddressToTokenMap(string filePath) : base(filePath)
    {
    }

    protected override Assembly LoadAssembly(string assemblyName) => 
        Assembly.Load(assemblyName);

    protected override MethodBase? ResolveMethod(Assembly? assembly, int token) =>
         assembly?.ManifestModule.ResolveMethod(token);
}
