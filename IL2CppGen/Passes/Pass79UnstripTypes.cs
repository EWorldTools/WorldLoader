using System.Linq;
using Il2CppInterop.Internal;
using Il2CppInterop.Generator.Contexts;
using Il2CppInterop.Generator.Utils;

using Mono.Cecil;

namespace Il2CppInterop.Generator.Passes;

internal static class Pass79UnstripTypes
{
    internal static void DoPass(RewriteGlobalContext context)
    {
        var typesUnstripped = 0;

        foreach (var unityAssembly in context.UnityAssemblies.Assemblies)
        {
            var processedAssembly = context.TryGetAssemblyByName(unityAssembly.Name.Name);
            if (processedAssembly == null)
            {
                var newAssembly = new UnhollowedAssemblyContext(context, unityAssembly,
                    AssemblyDefinition.CreateAssembly(unityAssembly.Name, unityAssembly.MainModule.Name,
                        ModuleKind.Dll));
                context.AddAssemblyContext(unityAssembly.Name.Name, newAssembly);
                processedAssembly = newAssembly;
            }

            var imports = processedAssembly.Imports;

            foreach (var unityType in unityAssembly.MainModule.Types)
                ProcessType(processedAssembly, unityType, null, imports, ref typesUnstripped);
        }

        Logger.Instance.LogTrace($"Unstripped {typesUnstripped} types");
    }

    private static void ProcessType(UnhollowedAssemblyContext processedAssembly, TypeDefinition unityType,
        TypeDefinition? enclosingNewType, RuntimeAssemblyReferences imports, ref int typesUnstripped)
    {
        if (unityType.Name == "<Module>")
            return;
        var newModule = processedAssembly.NewAssembly.MainModule;
        var processedType = enclosingNewType == null
            ? processedAssembly.TryGetTypeByName(unityType.FullName)?.NewType
            : enclosingNewType.NestedTypes.SingleOrDefault(it => it.Name == unityType.Name);
        if (unityType.IsEnum)
        {
            if (processedType != null) return;

            typesUnstripped++;
            var clonedType = CloneEnum(unityType, imports);
            if (enclosingNewType == null)
            {
                newModule.Types.Add(clonedType);
            }
            else
            {
                enclosingNewType.NestedTypes.Add(clonedType);
                clonedType.DeclaringType = enclosingNewType;
            }

            processedAssembly.RegisterTypeRewrite(new TypeRewriteContext(processedAssembly, null, clonedType));

            return;
        }

        if (processedType == null && !unityType.IsEnum && !HasNonBlittableFields(unityType) &&
            !unityType.HasGenericParameters) // restore all types even if it would be not entirely correct
        {
            typesUnstripped++;
            var clonedType = new TypeDefinition(unityType.Namespace, unityType.Name, ForcePublic(unityType.Attributes), unityType.BaseType == null ? null : newModule.ImportReference(unityType.BaseType));
            if (enclosingNewType == null)
            {
                newModule.Types.Add(clonedType);
            }
            else
            {
                enclosingNewType.NestedTypes.Add(clonedType);
                clonedType.DeclaringType = enclosingNewType;
            }

            processedAssembly.RegisterTypeRewrite(new TypeRewriteContext(processedAssembly, null, clonedType));
            processedType = clonedType;
        }

        foreach (var nestedUnityType in unityType.NestedTypes)
            ProcessType(processedAssembly, nestedUnityType, processedType, imports, ref typesUnstripped);
    }

    private static TypeDefinition CloneEnum(TypeDefinition sourceEnum, RuntimeAssemblyReferences imports)
    {
        var newType = new TypeDefinition(sourceEnum.Namespace, sourceEnum.Name, ForcePublic(sourceEnum.Attributes),
            imports.Module.Enum());
        foreach (var sourceEnumField in sourceEnum.Fields)
        {
            var newField = new FieldDefinition(sourceEnumField.Name, sourceEnumField.Attributes,
                sourceEnumField.Name == "value__"
                    ? imports.Module.ImportCorlibReference(sourceEnumField.FieldType.Namespace,
                        sourceEnumField.FieldType.Name)
                    : newType);
            newField.Constant = sourceEnumField.Constant;
            newType.Fields.Add(newField);
        }

        return newType;
    }

    private static bool HasNonBlittableFields(TypeDefinition type)
    {
        if (!type.IsValueType) return false;

        foreach (var fieldDefinition in type.Fields)
        {
            if (fieldDefinition.IsStatic || fieldDefinition.FieldType == type) continue;

            if (!fieldDefinition.FieldType.IsValueType)
                return true;

            if (fieldDefinition.FieldType.Namespace.StartsWith("System") &&
                HasNonBlittableFields(fieldDefinition.FieldType.Resolve()))
                return true;
        }

        return false;
    }

    private static TypeAttributes ForcePublic(TypeAttributes typeAttributes)
    {
        var visibility = typeAttributes & TypeAttributes.VisibilityMask;
        if (visibility == 0 || visibility == TypeAttributes.Public)
            return typeAttributes | TypeAttributes.Public;

        return (typeAttributes & ~TypeAttributes.VisibilityMask) | TypeAttributes.NestedPublic;
    }
}
