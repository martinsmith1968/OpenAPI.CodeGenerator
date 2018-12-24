using System;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.TemplateProviders.Implementation;

namespace OpenAPI.CodeGenerator.TemplateProviders
{
    public static class TemplateProviderFactory
    {
        public static ITemplateProvider GetTemplateProvider(TemplateProviderType templateProviderType)
        {
            switch (templateProviderType)
            {
                case TemplateProviderType.FileSystem:
                    return new FileSystemTemplateProvider();

                case TemplateProviderType.Resource:
                    return new ResourceTemplateProvider();

                default:
                    throw new ArgumentOutOfRangeException(nameof(templateProviderType), $"Invalid or unsupported {nameof(TemplateProviderType)}: {templateProviderType.ToString()}");
            }
        }
    }
}
