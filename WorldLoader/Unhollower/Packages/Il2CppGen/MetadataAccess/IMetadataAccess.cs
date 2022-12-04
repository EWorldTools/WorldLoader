using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace WorldLoader.Il2CppGen.Generator.MetadataAccess;

public interface IMetadataAccess : IDisposable
{
    IList<AssemblyDefinition> Assemblies { get; }

    AssemblyDefinition? GetAssemblyBySimpleName(string name);
    TypeDefinition? GetTypeByName(string assemblyName, string typeName);
}
