using Il2CppInterop.Internal;
using Il2CppInterop.Generator.Contexts;
using Il2CppInterop.Generator.Utils;
using Mono.Cecil;
using System;

namespace Il2CppInterop.Generator.Passes;

internal static class Pass40GenerateFieldAccessors
{
    internal static void DoPass(RewriteGlobalContext context)
    {
        foreach (var assemblyContext in context.Assemblies)
            foreach (var typeContext in assemblyContext.Types)
                foreach (var fieldContext in typeContext.Fields)
                {
                    if (typeContext.ComputedTypeSpecifics == TypeRewriteContext.TypeSpecifics.BlittableStruct &&
                        !fieldContext.OriginalField.IsStatic) continue;

                    var field = fieldContext.OriginalField;
                    var unmangleFieldName = fieldContext.UnmangledName;
                    PropertyDefinition property = null;
                    try
                    {
                        property = new PropertyDefinition(unmangleFieldName, PropertyAttributes.None,
                        assemblyContext.RewriteTypeRef(fieldContext.OriginalField.FieldType));
                    } catch (Exception E) { Logger.Instance.LogError($"Error During Pass 40 At var Property - {unmangleFieldName} {E}"); }
                    if (property == null) continue;
                    typeContext.NewType.Properties.Add(property);

                    FieldAccessorGenerator.MakeGetter(field, fieldContext, property, assemblyContext.Imports);
                    FieldAccessorGenerator.MakeSetter(field, fieldContext, property, assemblyContext.Imports);
                }
    }
}
