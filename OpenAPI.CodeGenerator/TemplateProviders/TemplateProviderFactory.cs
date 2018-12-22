using System;
using OpenAPI.CodeGenerator.Configuration;
using OpenAPI.CodeGenerator.TemplateProviders.Implementation;

namespace OpenAPI.CodeGenerator.TemplateProviders
{
    public static class TemplateProviderFactory
    {
        public static ITemplateProvider GetTemplateProvider(TemplateProviderType templateProviderType, string renderEngineName, string language)
        {
            switch (templateProviderType)
            {
                case TemplateProviderType.FileSystem:
                    return new FileSystemTemplateProvider(renderEngineName, language);

                case TemplateProviderType.Resource:
                    return new ResourceTemplateProvider(renderEngineName, language);

                default:
                    throw new ArgumentOutOfRangeException(nameof(templateProviderType), $"Invalid or unsupported {nameof(TemplateProviderType)}: {templateProviderType.ToString()}");
            }
        }
    }
}
