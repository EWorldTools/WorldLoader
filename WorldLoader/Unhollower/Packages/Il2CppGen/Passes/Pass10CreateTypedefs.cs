using System;
using System.Collections.Generic;
using System.Linq;
using WorldLoader.Il2CppGen.Internal;
using WorldLoader.Il2CppGen.Generator.Contexts;
using WorldLoader.Il2CppGen.Generator.Extensions;
using WorldLoader.Il2CppGen.Json;
using Mono.Cecil;
using WorldLoader.HookUtils;

namespace WorldLoader.Il2CppGen.Generator.Passes;

internal static class Pass10CreateTypedefs
{
    internal static void DoPass(RewriteGlobalContext context)
    {
        foreach (var assemblyContext in context.Assemblies)
            foreach (var type in assemblyContext.OriginalAssembly.MainModule.Types)
                if (type.Namespace != "Cpp2ILInjected" && type.Name != "<Module>")
                    ProcessType(type, assemblyContext, null, assemblyContext.OriginalAssembly.Name.Name);
    }

    private static void ProcessType(TypeDefinition type, UnhollowedAssemblyContext assemblyContext,
        TypeDefinition? parentType, string? AssemblyName = null)
    {

        var convertedTypeName = GetConvertedTypeName(assemblyContext.GlobalContext, type, parentType, AssemblyName);
        var newType =
            new TypeDefinition(
                convertedTypeName.Namespace ?? GetNamespace(type, assemblyContext),
                convertedTypeName.Name, AdjustAttributes(type.Attributes));
        newType.IsSequentialLayout = false;

        if (type.IsSealed && type.IsAbstract) // is static
            newType.IsSealed = newType.IsAbstract = true;

        if (parentType == null)
        {
            assemblyContext.NewAssembly.MainModule.Types.Add(newType);
        }
        else
        {
            parentType.NestedTypes.Add(newType);
            newType.DeclaringType = parentType;
        }

        foreach (var typeNestedType in type.NestedTypes)
            ProcessType(typeNestedType, assemblyContext, newType);

        assemblyContext.RegisterTypeRewrite(new TypeRewriteContext(assemblyContext, type, newType));
    }


    static string GetNamespace(TypeDefinition type, UnhollowedAssemblyContext assemblyContext)
    {
        if (type.Name is "<Module>" or "<PrivateImplementationDetails>" || type.DeclaringType is not null)
            return type.Namespace;
        else
            return type.Namespace.UnSystemify(assemblyContext.GlobalContext.Options);
    }

    internal static (string? Namespace, string Name) GetConvertedTypeName(
        RewriteGlobalContext assemblyContextGlobalContext, TypeDefinition type, TypeDefinition? enclosingType, string AssemblyName = null)
    {
        if (assemblyContextGlobalContext.Options.PassthroughNames)
            return (null, type.Name);

        if (type.Name.IsObfuscated(assemblyContextGlobalContext.Options))
        {
            var newNameBase = assemblyContextGlobalContext.RenamedTypes[type];
            var genericParametersCount = type.GenericParameters.Count;
            var renameGroup =
                assemblyContextGlobalContext.RenameGroups[
                    ((object)type.DeclaringType ?? type.Namespace, newNameBase, genericParametersCount)];
            var genericSuffix = genericParametersCount == 0 ? "" : "`" + genericParametersCount;
            var convertedTypeName = newNameBase +
                                    (renameGroup.Count == 1 ? "Unique" : renameGroup.IndexOf(type).ToString()) +
                                    genericSuffix;

            var fullName = enclosingType == null
                ? type.Namespace
                : enclosingType.GetNamespacePrefix() + "." + enclosingType.Name;
            if (assemblyContextGlobalContext.Options.DeObbJson != null)
                if (assemblyContextGlobalContext.Options.DeObbJson.Count > 0)
                {
                    var deobbsMap = CheckName(assemblyContextGlobalContext, type, AssemblyName);
                    if (deobbsMap != null)
                    {
                        if (type.Module.Types.Any(t => t.FullName == deobbsMap.AssemblyName))
                        {
                            Logger.Instance.LogWarning($"[Rename map issue] {deobbsMap.AssemblyName} already exists in {type.Module.Name} (mapped from {fullName}.{convertedTypeName})");
                            deobbsMap.AssemblyName += "_Duplicate";
                        }
                        var lastDotPosition = deobbsMap.AssemblyName.LastIndexOf(".");
                        if (lastDotPosition >= 0)
                        {
                            var ns = deobbsMap.AssemblyName.Substring(0, lastDotPosition);
                            var name = deobbsMap.AssemblyName.Substring(lastDotPosition + 1);
                            return (ns, name);
                        }
                    }
                }

            if (assemblyContextGlobalContext.Options.RenameMap.TryGetValue(fullName + "." + convertedTypeName,
                    out var newName))
            {
                Logger.Instance.LogWarning($"[Rename map] (mapped from {fullName}.{convertedTypeName})");
                if (type.Module.Types.Any(t => t.FullName == newName))
                {
                    Logger.Instance.LogWarning($"[Rename map issue] {newName} already exists in {type.Module.Name} (mapped from {fullName}.{convertedTypeName})");
                    newName += "_Duplicate";
                }

                var lastDotPosition = newName.LastIndexOf(".");
                if (lastDotPosition >= 0)
                {
                    var ns = newName.Substring(0, lastDotPosition);
                    var name = newName.Substring(lastDotPosition + 1);
                    return (ns, name);
                }

                convertedTypeName = newName;
            }

            return (null, convertedTypeName);
        }

        if (type.Name.IsInvalidInSource())
            return (null, type.Name.FilterInvalidInSourceChars());

        return (null, type.Name);
    }

    private static List<string> AlreadyPassed = new List<string>();

    internal static int Passes = 0;

    private static Deobb? CheckName(
        RewriteGlobalContext context, TypeDefinition typeDefinition, string AssemblyName = null)
    {
        //Console.WriteLine($"Checking {typeDefinition.Name}\n                        Methods");

        var settings = context.Options;
        var Methods = typeDefinition.Methods;
        var Fields = typeDefinition.Fields;
        if (settings.DeObbJson.Count > 0 && Methods != null) {
            foreach (var S in settings.DeObbJson) {
                int IsAssembly = 0;
                bool IsAssemblySkipped = false;
                if (!string.IsNullOrEmpty(AssemblyName) && !string.IsNullOrEmpty(S.AssemblyFile))
                    if (!AssemblyName.Contains(S.AssemblyFile)) IsAssemblySkipped = true;
   
                if (!IsAssemblySkipped)
                    if (S.WithMethods.Count > 0 || S.WithOutMethods.Count > 0)
                        foreach (var Method in Methods)
                        {
                            // TODO: Add a Way to get the UnObfuscated name of the Method and check that with the Deobb Maps for Easier Deobbcuation
                            //if (Method.Name.IsObfuscated(context.Options))
                            //{
                            //}
                            //else
                            if (S.WithOutMethods.Contains(Method.Name))
                            {
                                IsAssemblySkipped = true;
                                break;
                            }
                            if (S.WithMethods.Contains(Method.Name))
                                IsAssembly++;
                        }

                if (!IsAssemblySkipped)
                    if (S.WithFields.Count > 0 && Fields != null)
                    foreach (var Field in Fields) {
                        if (S.WithFields.Contains(Field.Name)) {
                                IsAssembly++;
                                Logs.Debug("Matched Field " + Field.FullName);
                            }
                            if (S.WithOutFields.Count > 0)
                        if (S.WithOutFields.Contains(Field.Name)) {
                            IsAssemblySkipped = true;
                            break;
                        }
                            if (S.WithProperties.Count > 0)
                                if (S.WithProperties.Contains(Field.FieldType.Name))
                                    IsAssembly++;
                            if (S.WithOutProperties.Count > 0)
                                if (S.WithOutProperties.Contains(Field.FieldType.Name))
                                    IsAssemblySkipped = true;
                        }
                if (!IsAssemblySkipped)
                    if (!string.IsNullOrEmpty(S.Type))
                        if (typeDefinition.BaseType.FullName == S.Type)
                            IsAssembly++;

                if (IsAssembly == 0) continue;
                if (IsAssemblySkipped) {
                    continue;
                }

                var Count = S.WithMethods.Count
                    + S.WithFields.Count
                    + S.WithProperties.Count;
                if (!string.IsNullOrEmpty(S.Type))
                    Count++;

                if (IsAssembly == Count) {
                    Logs.Log($"{typeDefinition.Name} => {S.AssemblyName} (Count {Count})", "Assembly Generation", "Deobfuscation");
                    settings.DeObbJson.Remove(S);
                    Passes++;
                    return S;
                }
                else
                    Logs.Debug($"Not Writing {typeDefinition.Name} with {S.AssemblyName} Due to the count being off ({IsAssembly} /{Count})");
            }
        }
        return null;

    }

    private static TypeAttributes AdjustAttributes(TypeAttributes typeAttributes)
    {
        typeAttributes |= TypeAttributes.BeforeFieldInit;
        typeAttributes &= ~(TypeAttributes.Abstract | TypeAttributes.Interface);

        var visibility = typeAttributes & TypeAttributes.VisibilityMask;
        if (visibility == 0 || visibility == TypeAttributes.Public)
            return typeAttributes | TypeAttributes.Public;

        return (typeAttributes & ~TypeAttributes.VisibilityMask) | TypeAttributes.NestedPublic;
    }
}
