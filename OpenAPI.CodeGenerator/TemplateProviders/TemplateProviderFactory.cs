using System;
using OpenAPI.CodeGenerator.Arguments;

namespace OpenAPI.CodeGenerator.TemplateProviders
{
    public static class TemplateProviderFactory
    {
        public static ITemplateProvider GetTemplateProvider(TemplateProviderType templateProviderType, string language)
        {
            switch (templateProviderType)
            {
                case TemplateProviderType.FileSystem:
                    return new FileSystemTemplateProvider(language);

                case TemplateProviderType.Resource:
                    return new ResourceTemplateProvider(language);

                default:
                    throw new ArgumentOutOfRangeException(nameof(templateProviderType), $"Invalid or unsupported {nameof(TemplateProviderType)}: {templateProviderType.ToString()}");
            }
        }
    }
}
