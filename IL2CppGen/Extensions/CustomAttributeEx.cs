using System.Globalization;
using System.Linq;
using Mono.Cecil;

namespace Il2CppInterop.Generator.Extensions;

public static class CustomAttributeEx
{
    public static long ExtractOffset(this ICustomAttributeProvider originalMethod)
    {
        return Extract(originalMethod, "AddressAttribute", "Offset");
    }

    public static long ExtractRva(this ICustomAttributeProvider originalMethod)
    {
        return Extract(originalMethod, "AddressAttribute", "RVA");
    }

    public static long ExtractToken(this ICustomAttributeProvider originalMethod)
    {
        return Extract(originalMethod, "TokenAttribute", "Token");
    }

    private static long Extract(this ICustomAttributeProvider originalMethod, string attributeName,
        string parameterName)
    {
        var addressAttribute =
            originalMethod.CustomAttributes.SingleOrDefault(it => it.AttributeType.Name == attributeName);
        var rvaField = addressAttribute?.Fields.SingleOrDefault(it => it.Name == parameterName);

        if (rvaField?.Name == null) return 0;

        var addressString = (string)rvaField.Value.Argument.Value;
        long.TryParse(addressString.Substring(2), NumberStyles.HexNumber, null, out var address);
        return address;
    }
}
