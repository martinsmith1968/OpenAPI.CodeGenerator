using System;
using DNX.Helpers.Strings;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Common.Extensions
{
    public static class CommandExtensions
    {
        public static string GetCommandName(this Type type)
        {
            return type == null
                ? null
                : type.Name.RemoveEndsWith(typeof(ICommand).GetNonInterfaceName());
        }

        public static string GetCommandName(this ICommand command)
        {
            return command?.GetType().GetCommandName();
        }
    }
}
