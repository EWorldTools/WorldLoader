using System;
using System.Collections.Generic;
using WorldLoader.Il2CppGen.Generator.Utils;
using Mono.Cecil;

namespace WorldLoader.Il2CppGen.Generator.Contexts;

public class TypeRewriteContext
{
    public enum TypeSpecifics
    {
        NotComputed,
        Computing,
        ReferenceType,
        BlittableStruct,
        NonBlittableStruct
    }

    internal readonly UnhollowedAssemblyContext AssemblyContext;

    private readonly Dictionary<FieldDefinition, FieldRewriteContext> myFieldContexts = new();
    private readonly Dictionary<MethodDefinition, MethodRewriteContext> myMethodContexts = new();
    private readonly Dictionary<string, MethodRewriteContext> myMethodContextsByName = new();
    public readonly TypeDefinition NewType;

    public readonly bool OriginalNameWasObfuscated;
    public readonly TypeDefinition OriginalType;

    public TypeSpecifics ComputedTypeSpecifics;

    internal TypeRewriteContext(UnhollowedAssemblyContext assemblyContext, TypeDefinition originalType,
        TypeDefinition newType)
    {
        AssemblyContext = assemblyContext ?? throw new ArgumentNullException(nameof(assemblyContext));
        OriginalType = originalType;
        NewType = newType ?? throw new ArgumentNullException(nameof(newType));

        if (OriginalType == null) return;

        OriginalNameWasObfuscated = OriginalType.Name != NewType.Name;
        if (OriginalNameWasObfuscated)
            NewType.CustomAttributes.Add(new CustomAttribute(assemblyContext.Imports.ObfuscatedNameAttributector.Value)
            {
                ConstructorArguments =
                    {new CustomAttributeArgument(assemblyContext.Imports.Module.String(), originalType.FullName)}
            });

        if (!OriginalType.IsValueType)
            ComputedTypeSpecifics = TypeSpecifics.ReferenceType;
        else if (OriginalType.IsEnum)
            ComputedTypeSpecifics = TypeSpecifics.BlittableStruct;
        else if (OriginalType.HasGenericParameters)
            ComputedTypeSpecifics = TypeSpecifics.NonBlittableStruct; // not reference type, covered by first if
    }

    public FieldReference ClassPointerFieldRef { get; private set; }
    public TypeReference SelfSubstitutedRef { get; private set; }

    public IEnumerable<FieldRewriteContext> Fields => myFieldContexts.Values;
    public IEnumerable<MethodRewriteContext> Methods => myMethodContexts.Values;

    public void AddMembers()
    {
        if (NewType.HasGenericParameters)
        {
            var genericInstanceType = new GenericInstanceType(NewType);
            foreach (var newTypeGenericParameter in NewType.GenericParameters)
                genericInstanceType.GenericArguments.Add(newTypeGenericParameter);
            SelfSubstitutedRef = NewType.Module.ImportReference(genericInstanceType);
            var genericTypeRef = new GenericInstanceType(AssemblyContext.Imports.Il2CppClassPointerStore)
            { GenericArguments = { SelfSubstitutedRef } };
            ClassPointerFieldRef = new FieldReference("NativeClassPtr", AssemblyContext.Imports.Module.IntPtr(),
                NewType.Module.ImportReference(genericTypeRef));
        }
        else
        {
            SelfSubstitutedRef = NewType;
            var genericTypeRef = new GenericInstanceType(AssemblyContext.Imports.Il2CppClassPointerStore);
            if (OriginalType.IsPrimitive || OriginalType.FullName == "System.String")
                genericTypeRef.GenericArguments.Add(
                    NewType.Module.ImportCorlibReference(OriginalType.Namespace, OriginalType.Name));
            else
                genericTypeRef.GenericArguments.Add(SelfSubstitutedRef);
            ClassPointerFieldRef = new FieldReference("NativeClassPtr", AssemblyContext.Imports.Module.IntPtr(),
                NewType.Module.ImportReference(genericTypeRef));
        }

        if (OriginalType.IsEnum) return;

        var renamedFieldCounts = new Dictionary<string, int>();

        foreach (var originalTypeField in OriginalType.Fields)
            myFieldContexts[originalTypeField] = new FieldRewriteContext(this, originalTypeField, renamedFieldCounts);

        var hasExtensionMethods = false;

        foreach (var originalTypeMethod in OriginalType.Methods)
        {
            if (originalTypeMethod.Name == ".cctor") continue;
            if (originalTypeMethod.Name == ".ctor" && originalTypeMethod.Parameters.Count == 1 &&
                originalTypeMethod.Parameters[0].ParameterType.FullName == "System.IntPtr") continue;
            if (originalTypeMethod.HasOverrides) continue;

            var methodRewriteContext = new MethodRewriteContext(this, originalTypeMethod);
            myMethodContexts[originalTypeMethod] = methodRewriteContext;
            myMethodContextsByName[originalTypeMethod.Name] = methodRewriteContext;

            if (methodRewriteContext.HasExtensionAttribute) hasExtensionMethods = true;
        }

        if (hasExtensionMethods)
            NewType.CustomAttributes.Add(new CustomAttribute(AssemblyContext.Imports.Module.ExtensionAttributeCtor()));
    }

    public FieldRewriteContext GetFieldByOldField(FieldDefinition field)
    {
        return myFieldContexts[field];
    }

    public MethodRewriteContext GetMethodByOldMethod(MethodDefinition method)
    {
        return myMethodContexts[method];
    }

    public MethodRewriteContext? TryGetMethodByOldMethod(MethodDefinition method)
    {
        return myMethodContexts.TryGetValue(method, out var result) ? result : null;
    }

    public MethodRewriteContext? TryGetMethodByName(string name)
    {
        return myMethodContextsByName.TryGetValue(name, out var result) ? result : null;
    }

    public MethodRewriteContext? TryGetMethodByUnityAssemblyMethod(MethodDefinition method)
    {
        foreach (var methodRewriteContext in myMethodContexts)
        {
            var originalMethod = methodRewriteContext.Value.OriginalMethod;
            if (originalMethod.Name != method.Name) continue;
            if (originalMethod.Parameters.Count != method.Parameters.Count) continue;
            var badMethod = false;
            for (var i = 0; i < originalMethod.Parameters.Count; i++)
                if (originalMethod.Parameters[i].ParameterType.FullName != method.Parameters[i].ParameterType.FullName)
                {
                    badMethod = true;
                    break;
                }

            if (badMethod) continue;

            return methodRewriteContext.Value;
        }

        return null;
    }

    public FieldRewriteContext? TryGetFieldByUnityAssemblyField(FieldDefinition field)
    {
        foreach (var fieldRewriteContext in myFieldContexts)
        {
            var originalField = fieldRewriteContext.Value.OriginalField;
            if (originalField.Name != field.Name) continue;

            if (originalField.FieldType.FullName != field.FieldType.FullName)
                continue;

            return fieldRewriteContext.Value;
        }

        return null;
    }
}
