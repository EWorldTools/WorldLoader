using Il2CppInterop.Internal;
using Il2CppInterop.Generator.Contexts;

using Mono.Cecil;

namespace Il2CppInterop.Generator.Passes;

internal static class Pass80UnstripFields
{
    internal static void DoPass(RewriteGlobalContext context)
    {
        var fieldsUnstripped = 0;
        var fieldsIgnored = 0;

        foreach (var unityAssembly in context.UnityAssemblies.Assemblies)
        {
            var processedAssembly = context.TryGetAssemblyByName(unityAssembly.Name.Name);
            if (processedAssembly == null) continue;
            var imports = processedAssembly.Imports;

            foreach (var unityType in unityAssembly.MainModule.Types)
            {
                var processedType = processedAssembly.TryGetTypeByName(unityType.FullName);
                if (processedType == null) continue;

                if (!unityType.IsValueType || unityType.IsEnum)
                    continue;

                foreach (var unityField in unityType.Fields)
                {
                    if (unityField.IsStatic && !unityField.HasConstant) continue;
                    if (processedType.NewType.IsExplicitLayout && !unityField.IsStatic) continue;

                    var processedField = processedType.TryGetFieldByUnityAssemblyField(unityField);
                    if (processedField != null) continue;

                    var fieldType =
                        Pass80UnstripMethods.ResolveTypeInNewAssemblies(context, unityField.FieldType, imports);
                    if (fieldType == null)
                    {
                        Logger.Instance.LogTrace($"Field {unityField.ToString()} on type {unityType.FullName} has unsupported type {unityField.FieldType.ToString()}, the type will be unusable");
                        fieldsIgnored++;
                        continue;
                    }

                    var newField = new FieldDefinition(unityField.Name,
                        (unityField.Attributes & ~FieldAttributes.FieldAccessMask) | FieldAttributes.Public, fieldType);

                    if (unityField.HasConstant) newField.Constant = unityField.Constant;

                    processedType.NewType.Fields.Add(newField);

                    fieldsUnstripped++;
                }
            }
        }

        Logger.Instance.LogInformation($"Restored {fieldsUnstripped} fields");
        Logger.Instance.LogInformation($"Failed to restore {fieldsIgnored} fields");
    }
}
