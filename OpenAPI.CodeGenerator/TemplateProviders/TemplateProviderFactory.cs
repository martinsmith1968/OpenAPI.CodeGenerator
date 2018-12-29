using System;
using Autofac;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.TemplateProviders
{
    public class TemplateProviderFactory : ITemplateProviderFactory
    {
        public ITemplateProvider GetTemplateProvider(TemplateProviderType templateProviderType)
        {
            var templateProvider = Program.Container.ResolveOptionalKeyed<ITemplateProvider>(templateProviderType);
            if (templateProvider == null)
                throw new ArgumentOutOfRangeException(nameof(templateProviderType), $"Invalid or unsupported {nameof(TemplateProviderType)}: {templateProviderType.ToString()}");

            return templateProvider;
        }
    }
}
