using System;

namespace WorldLoader.Attributes;

[AttributeUsage(AttributeTargets.All, Inherited = false)]
public class ObfuscatedNameAttribute : Attribute
{
    public readonly string ObfuscatedName;

    public ObfuscatedNameAttribute(string obfuscatedName)
    {
        ObfuscatedName = obfuscatedName;
    }
}
