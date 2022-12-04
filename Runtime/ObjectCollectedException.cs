using System;

namespace Il2CppGen.Runtime;

public class ObjectCollectedException : Exception
{
    public ObjectCollectedException(string message) : base(message)
    {
    }
}
