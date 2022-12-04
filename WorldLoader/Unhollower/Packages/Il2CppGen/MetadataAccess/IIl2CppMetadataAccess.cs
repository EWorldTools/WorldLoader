using System.Collections.Generic;
using Mono.Cecil;

namespace WorldLoader.Il2CppGen.Generator.MetadataAccess;

public interface IIl2CppMetadataAccess : IMetadataAccess
{
    IList<GenericInstanceType>? GetKnownInstantiationsFor(TypeDefinition genericDeclaration);
    string? GetStringStoredAtAddress(long offsetInMemory);
    MethodReference? GetMethodRefStoredAt(long offsetInMemory);
}
