using System;
using DNX.Helpers.Strings;

namespace OpenAPI.CodeGenerator.Common.Extensions
{
    public static class TypeExtensions
    {
        public static string GetNonInterfaceName(this Type type)
        {
            return type == null
                ? null
                : type.Name.RemoveStartsWith("I");
        }
    }
}
