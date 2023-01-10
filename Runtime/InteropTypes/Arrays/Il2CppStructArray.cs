using System;

namespace Il2CppInterop.Runtime.InteropTypes.Arrays;

public class Il2CppStructArray<T> : Il2CppArrayBase<T> where T : unmanaged
{
    static Il2CppStructArray()
    {
        StaticCtorBody(typeof(Il2CppStructArray<T>));
    }

    public Il2CppStructArray(IntPtr nativeObject) : base(nativeObject)
    {
    }

    public Il2CppStructArray(long size) : base(AllocateArray(size))
    {
    }

    public Il2CppStructArray(T[] arr) : base(AllocateArray(arr.Length))
    {
        for (var i = 0; i < arr.Length; i++)
            this[i] = arr[i];
    }

    public override unsafe T this[int index]
    {
        get
        {
            if (index < 0 || index >= Length)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Array index may not be negative or above length of the array");
            var arrayStartPointer = IntPtr.Add(Pointer, 4 * IntPtr.Size);
            return ((T*)arrayStartPointer.ToPointer())[index];
        }
        set
        {
            if (index < 0 || index >= Length)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Array index may not be negative or above length of the array");
            var arrayStartPointer = IntPtr.Add(Pointer, 4 * IntPtr.Size);
            ((T*)arrayStartPointer.ToPointer())[index] = value;
        }
    }

    public static implicit operator Il2CppStructArray<T>(T[] arr)
    {
        if (arr == null) return null;

        return new Il2CppStructArray<T>(arr);
    }

    private static IntPtr AllocateArray(long size)
    {
        if (size < 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Array size must not be negative");

        var elementTypeClassPointer = Il2CppClassPointerStore<T>.NativeClassPtr;
        if (elementTypeClassPointer == IntPtr.Zero)
            throw new ArgumentException(
                $"{nameof(Il2CppStructArray<T>)} requires an Il2Cpp reference type, which {typeof(T)} isn't");
        return IL2CPP.il2cpp_array_new(elementTypeClassPointer, (ulong)size);
    }
}
