using System;
using DNX.Helpers.Strings;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Common.Extensions
{
    public static class LanguageExtensions
    {
        public static string GetLanguageName(this Type type)
        {
            return type == null
                ? null
                : type.Name.RemoveEndsWith(nameof(ILanguage).RemoveStartsWith("I"));
        }

        public static string GetLanguageName(this ILanguage language)
        {
            return language?.GetType().GetLanguageName();
        }
    }
}
