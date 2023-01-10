using System.Reflection;
using Il2CppInterop.Generator.Contexts;
using Il2CppInterop.Generator.Extensions;
using Il2CppInterop.Generator.Utils;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MethodAttributes = Mono.Cecil.MethodAttributes;

namespace Il2CppInterop.Generator.Passes;

internal static class Pass30GenerateGenericMethodStoreConstructors
{
    internal static void DoPass(RewriteGlobalContext context)
    {
        foreach (var assemblyContext in context.Assemblies)
            foreach (var typeContext in assemblyContext.Types)
                foreach (var methodContext in typeContext.Methods)
                {
                    var oldMethod = methodContext.OriginalMethod;

                    var storeType = methodContext.GenericInstantiationsStore;
                    if (storeType != null)
                    {
                        var cctor = new MethodDefinition(".cctor",
                            MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.SpecialName |
                            MethodAttributes.RTSpecialName | MethodAttributes.HideBySig,
                            assemblyContext.Imports.Module.Void());
                        storeType.Methods.Add(cctor);

                        var ctorBuilder = cctor.Body.GetILProcessor();

                        var il2CppTypeTypeRewriteContext = assemblyContext.GlobalContext
                            .GetAssemblyByName("mscorlib").GetTypeByName("System.Type");
                        var il2CppSystemTypeRef =
                            assemblyContext.NewAssembly.MainModule.ImportReference(il2CppTypeTypeRewriteContext.NewType);

                        var il2CppMethodInfoTypeRewriteContext = assemblyContext.GlobalContext
                            .GetAssemblyByName("mscorlib").GetTypeByName("System.Reflection.MethodInfo");
                        var il2CppSystemReflectionMethodInfoRef =
                            assemblyContext.NewAssembly.MainModule.ImportReference(il2CppMethodInfoTypeRewriteContext.NewType);

                        ctorBuilder.Emit(OpCodes.Ldsfld, methodContext.NonGenericMethodInfoPointerField);
                        ctorBuilder.Emit(OpCodes.Ldsfld, typeContext.ClassPointerFieldRef);
                        ctorBuilder.Emit(OpCodes.Call, assemblyContext.Imports.IL2CPP_il2cpp_method_get_object.Value);
                        ctorBuilder.Emit(OpCodes.Newobj,
                            new MethodReference(".ctor", assemblyContext.Imports.Module.Void(),
                                il2CppSystemReflectionMethodInfoRef)
                            {
                                HasThis = true,
                                Parameters = { new ParameterDefinition(assemblyContext.Imports.Module.IntPtr()) }
                            });

                        ctorBuilder.EmitLdcI4(oldMethod.GenericParameters.Count);

                        ctorBuilder.Emit(OpCodes.Newarr, il2CppSystemTypeRef);

                        for (var i = 0; i < oldMethod.GenericParameters.Count; i++)
                        {
                            ctorBuilder.Emit(OpCodes.Dup);
                            ctorBuilder.EmitLdcI4(i);

                            var param = storeType.GenericParameters[i];
                            var storeRef = new GenericInstanceType(assemblyContext.Imports.Il2CppClassPointerStore)
                            { GenericArguments = { param } };
                            var fieldRef = new FieldReference(
                                "NativeClassPtr",
                                assemblyContext.Imports.Module.IntPtr(), storeRef);
                            ctorBuilder.Emit(OpCodes.Ldsfld, fieldRef);

                            ctorBuilder.Emit(OpCodes.Call, assemblyContext.Imports.IL2CPP_il2cpp_class_get_type.Value);

                            ctorBuilder.Emit(OpCodes.Call,
                                new MethodReference("internal_from_handle", il2CppSystemTypeRef,
                                        il2CppSystemTypeRef)
                                { Parameters = { new ParameterDefinition(assemblyContext.Imports.Module.IntPtr()) } });
                            ctorBuilder.Emit(OpCodes.Stelem_Ref);
                        }

                        var il2CppTypeArray = new GenericInstanceType(assemblyContext.Imports.Il2CppReferenceArray)
                        { GenericArguments = { il2CppSystemTypeRef } };
                        ctorBuilder.Emit(OpCodes.Newobj,
                            new MethodReference(".ctor", assemblyContext.Imports.Module.Void(), il2CppTypeArray)
                            {
                                HasThis = true,
                                Parameters =
                                {
                            new ParameterDefinition(
                                new ArrayType(assemblyContext.Imports.Il2CppReferenceArray.GenericParameters[0]))
                                }
                            });
                        ctorBuilder.Emit(OpCodes.Call,
                            new MethodReference(nameof(MethodInfo.MakeGenericMethod), il2CppSystemReflectionMethodInfoRef,
                                    il2CppSystemReflectionMethodInfoRef)
                            { HasThis = true, Parameters = { new ParameterDefinition(il2CppTypeArray) } });
                        ctorBuilder.Emit(OpCodes.Call, assemblyContext.Imports.IL2CPP_Il2CppObjectBaseToPtrNotNull.Value);

                        ctorBuilder.Emit(OpCodes.Call, assemblyContext.Imports.IL2CPP_il2cpp_method_get_from_reflection.Value);
                        ctorBuilder.Emit(OpCodes.Stsfld,
                            new FieldReference("Pointer", assemblyContext.Imports.Module.IntPtr(),
                                methodContext.GenericInstantiationsStoreSelfSubstRef));

                        ctorBuilder.Emit(OpCodes.Ret);
                    }
                }
    }
}
