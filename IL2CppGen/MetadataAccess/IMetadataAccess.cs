using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Il2CppInterop.Generator.MetadataAccess;

public interface IMetadataAccess : IDisposable
{
    IList<AssemblyDefinition> Assemblies { get; }

    AssemblyDefinition? GetAssemblyBySimpleName(string name);
    TypeDefinition? GetTypeByName(string assemblyName, string typeName);
}
