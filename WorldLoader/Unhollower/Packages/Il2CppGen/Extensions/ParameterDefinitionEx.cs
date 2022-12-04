﻿using Mono.Cecil;
using System;
using System.Linq;

namespace WorldLoader.Il2CppGen.Generator.Extensions;

internal static class ParameterDefinitionEx
{
    public static bool IsParamsArray(this ParameterDefinition self)
    {
        return self.ParameterType is ArrayType { Rank: 1 } && self.CustomAttributes.Any(attribute =>
            attribute.AttributeType.FullName == typeof(ParamArrayAttribute).FullName);
    }
}
