using Il2CppInterop.Generator.Contexts;
using Il2CppInterop.Generator.Extensions;
using Il2CppInterop.Generator.Utils;
using Mono.Cecil;

namespace Il2CppInterop.Generator.Passes;

internal static class Pass12FillTypedefs
{
    internal static void DoPass(RewriteGlobalContext context)
    {
        foreach (var assemblyContext in context.Assemblies)
            foreach (var typeContext in assemblyContext.Types)
            {
                foreach (var originalParameter in typeContext.OriginalType.GenericParameters)
                {
                    var newParameter = new GenericParameter(originalParameter.Name, typeContext.NewType);
                    typeContext.NewType.GenericParameters.Add(newParameter);
                    newParameter.Attributes = originalParameter.Attributes.StripValueTypeConstraint();
                }

                if (typeContext.OriginalType.IsEnum)
                    typeContext.NewType.BaseType = assemblyContext.Imports.Module.Enum();
                else if (typeContext.ComputedTypeSpecifics == TypeRewriteContext.TypeSpecifics.BlittableStruct)
                    typeContext.NewType.BaseType = assemblyContext.Imports.Module.ValueType();
            }

        // Second pass is explicitly done after first to account for rewriting of generic base types - value-typeness is important there
        foreach (var assemblyContext in context.Assemblies)
            foreach (var typeContext in assemblyContext.Types)
                if (!typeContext.OriginalType.IsEnum && typeContext.ComputedTypeSpecifics !=
                    TypeRewriteContext.TypeSpecifics.BlittableStruct)
                    typeContext.NewType.BaseType = assemblyContext.RewriteTypeRef(typeContext.OriginalType.BaseType);
    }
}
