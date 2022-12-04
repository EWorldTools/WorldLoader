using System.Reflection;
using System.Reflection.Emit;
using System;
using System.Linq;
namespace WorldLoader.Il2CppGen.Internal;

public static class Il2CppGenUtils
{
    private static FieldInfo? GetFieldInfoFromMethod(MethodBase method, string prefix)
    {
        var body = method.GetMethodBody();
        if (body == null) throw new ArgumentException("Target method may not be abstract");
        var methodModule = method.DeclaringType.Assembly.Modules.Single();
        foreach (var (opCode, opArg) in MiniIlParser.Decode(body.GetILAsByteArray()))
        {
            if (opCode != OpCodes.Ldsfld) continue;
            var fieldInfo = methodModule.ResolveField((int)opArg);
            if (fieldInfo?.FieldType != typeof(IntPtr) || !fieldInfo.Name.StartsWith(prefix)) continue;
            return fieldInfo;
        }

        return null;
    }


    public static T Il2CppObjectPtrToIl2CppObject<T>(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero)
            throw new NullReferenceException("The ptr cannot be IntPtr.Zero.");
        return (T)typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(IntPtr) }, new ParameterModifier[0]).Invoke(new object[] { ptr });
    }

    public static FieldInfo GetIl2CppMethodInfoPointerFieldForGeneratedMethod(MethodBase method)
    {
        return GetFieldInfoFromMethod(method, "NativeMethodInfoPtr_");
    }

    public static FieldInfo GetIl2CppFieldInfoPointerFieldForGeneratedFieldAccessor(MethodBase method)
    {
        return GetFieldInfoFromMethod(method, "NativeFieldInfoPtr_");
    }
}
