using WorldLoader.Il2CppGen.Generator.Contexts;
using WorldLoader.Il2CppGen.Generator.Utils;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace WorldLoader.Il2CppGen.Generator.Passes;

internal static class Pass25GenerateNonBlittableValueTypeDefaultCtors
{
    internal static void DoPass(RewriteGlobalContext context)
    {
        foreach (var assemblyContext in context.Assemblies)
            foreach (var typeContext in assemblyContext.Types)
            {
                if (typeContext.ComputedTypeSpecifics !=
                    TypeRewriteContext.TypeSpecifics.NonBlittableStruct) continue;

                var emptyCtor = new MethodDefinition(".ctor",
                    MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName |
                    MethodAttributes.HideBySig, assemblyContext.Imports.Module.Void());

                typeContext.NewType.Methods.Add(emptyCtor);

                var local0 = new VariableDefinition(assemblyContext.Imports.Module.IntPtr());
                emptyCtor.Body.Variables.Add(local0);

                var bodyBuilder = emptyCtor.Body.GetILProcessor();
                bodyBuilder.Emit(OpCodes.Ldsfld, typeContext.ClassPointerFieldRef);
                bodyBuilder.Emit(OpCodes.Ldc_I4_0);
                bodyBuilder.Emit(OpCodes.Conv_U);
                bodyBuilder.Emit(OpCodes.Call, assemblyContext.Imports.IL2CPP_il2cpp_class_value_size.Value);
                bodyBuilder.Emit(OpCodes.Conv_U);
                bodyBuilder.Emit(OpCodes.Localloc);
                bodyBuilder.Emit(OpCodes.Stloc_0);
                bodyBuilder.Emit(OpCodes.Ldarg_0);
                bodyBuilder.Emit(OpCodes.Ldsfld, typeContext.ClassPointerFieldRef);
                bodyBuilder.Emit(OpCodes.Ldloc_0);
                bodyBuilder.Emit(OpCodes.Call, assemblyContext.Imports.IL2CPP_il2cpp_value_box.Value);
                bodyBuilder.Emit(OpCodes.Call,
                    new MethodReference(".ctor", assemblyContext.Imports.Module.Void(), typeContext.NewType.BaseType)
                    {
                        HasThis = true,
                        Parameters = { new ParameterDefinition(assemblyContext.Imports.Module.IntPtr()) }
                    });
                bodyBuilder.Emit(OpCodes.Ret);
            }
    }
}
