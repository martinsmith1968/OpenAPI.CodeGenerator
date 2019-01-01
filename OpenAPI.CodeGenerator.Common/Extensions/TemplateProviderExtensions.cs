using System;
using DNX.Helpers.Strings;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Common.Extensions
{
    public static class TemplateProviderExtensions
    {
        public static string GetTemplateProviderName(this Type type)
        {
            return type == null
                ? null
                : type.Name.RemoveEndsWith(nameof(ITemplateProvider).RemoveStartsWith("I"));
        }

        public static string GetTemplateProviderName(this ITemplateProvider templateProvider)
        {
            return templateProvider?.GetType().GetTemplateProviderName();
        }
    }
}
