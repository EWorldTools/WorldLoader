using System;
using System.Runtime.InteropServices;
using Il2CppGen.Runtime.Runtime;
using WorldLoader.HookUtils;
using WorldLoader.Il2CppGen.Internal;

namespace Il2CppGen.Runtime.Injection;

public unsafe partial class ClassInjector
{
    public static void Dump<T>()
    {
        Dump((Il2CppClass*)Il2CppClassPointerStore<T>.NativeClassPtr);
    }

    private static string ToString(Il2CppClass* il2CppClass)
    {
        if (il2CppClass == default) return "null";
        var classStruct = UnityVersionHandler.Wrap(il2CppClass);
        return $"{Marshal.PtrToStringAnsi(classStruct.Namespace)}.{Marshal.PtrToStringAnsi(classStruct.Name)}";
    }

    private static string ToString(Il2CppTypeStruct* il2CppType)
    {
        if (il2CppType == default) return "null";
        return Marshal.PtrToStringAnsi(IL2CPP.il2cpp_type_get_name((IntPtr)il2CppType));
    }

    public static void Dump(Il2CppClass* il2CppClass)
    {
        if (il2CppClass == default) throw new ArgumentNullException(nameof(il2CppClass));

        InjectorHelpers.Setup();
        InjectorHelpers.ClassInit(il2CppClass);

        var classStruct = UnityVersionHandler.Wrap(il2CppClass);

        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $"Dumping {classStruct.Pointer:X}");

        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" Namespace = {Marshal.PtrToStringAnsi(classStruct.Namespace)}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" Name = { Marshal.PtrToStringAnsi(classStruct.Name)}");

        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" Parent = {ToString(classStruct.Parent)}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" InstanceSize = {classStruct.InstanceSize}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" NativeSize = {classStruct.NativeSize}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" ActualSize = {classStruct.ActualSize}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" Flags = {classStruct.Flags}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" ValueType = {classStruct.ValueType}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" EnumType = {classStruct.EnumType}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" IsGeneric = {classStruct.IsGeneric}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" Initialized = {classStruct.Initialized}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" InitializedAndNoError = {classStruct.InitializedAndNoError}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" SizeInited = {classStruct.SizeInited}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" HasFinalize = {classStruct.HasFinalize}");
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" IsVtableInitialized = {classStruct.IsVtableInitialized}");

        var vtable = (VirtualInvokeData*)classStruct.VTable;
        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" VTable ({classStruct.VtableCount}):");
        for (var i = 0; i < classStruct.VtableCount; i++)
        {
            var virtualInvokeData = vtable![i];
            var methodName = virtualInvokeData.method == default ? "<null>" : Marshal.PtrToStringAnsi(UnityVersionHandler.Wrap(virtualInvokeData.method).Name);

            "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $"  [{i}] {methodName} - {(virtualInvokeData.methodPtr == default ? " < null > " : virtualInvokeData.methodPtr)}");
        }

        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" Fields ({classStruct.FieldCount}):");
        for (var i = 0; i < classStruct.FieldCount; i++)
        {
            var field = UnityVersionHandler.Wrap(classStruct.Fields + i * UnityVersionHandler.FieldInfoSize());

            "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $"  [{i}] {ToString(field.Type)} {Marshal.PtrToStringAnsi(field.Name)} - {field.Offset}");
        }

        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" Methods ({classStruct.MethodCount}):");
        for (var i = 0; i < classStruct.MethodCount; i++)
        {
            var method = UnityVersionHandler.Wrap(classStruct.Methods[i]);

            "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, "  [{i}] {ToString(method.ReturnType)} {Marshal.PtrToStringAnsi(method.Name)}({method.ParametersCount}), {method.Flags}, {method.Slot}");
        }

        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" ImplementedInterfaces ({classStruct.InterfaceCount}):");
        for (var i = 0; i < classStruct.InterfaceCount; i++)
        {
            var @interface = UnityVersionHandler.Wrap(classStruct.ImplementedInterfaces[i]);

            "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $"  [{i}] {Marshal.PtrToStringAnsi(@interface.Name)}");
        }

        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" InterfaceOffsets ({classStruct.InterfaceOffsetsCount}):");
        for (var i = 0; i < classStruct.InterfaceOffsetsCount; i++)
        {
            var pair = classStruct.InterfaceOffsets[i];
            var @interface = UnityVersionHandler.Wrap(pair.interfaceType);

            "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $"  [{i}] {pair.offset} - {Marshal.PtrToStringAnsi(@interface.Name)}");
        }

        "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $" TypeHierarchy ({classStruct.TypeHierarchyDepth}):");
        for (var i = 0; i < classStruct.TypeHierarchyDepth; i++)
        {
            var @interface = UnityVersionHandler.Wrap(classStruct.TypeHierarchy[i]);

            "[ClassInjection Debug]".WriteLineToConsole(ConsoleColor.Gray, $"  [{i}] {Marshal.PtrToStringAnsi(@interface.Name)}");
        }
    }
}
