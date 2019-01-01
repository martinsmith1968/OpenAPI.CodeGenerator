using System;
using System.Collections.Generic;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Factories
{
    public class TemplateProviderFactory : ITemplateProviderFactory
    {
        public IEnumerable<ITemplateProvider> TemplateProviders { get; }

        public TemplateProviderFactory(IEnumerable<ITemplateProvider> templateProviders)
        {
            TemplateProviders = templateProviders;
        }

        public ITemplateProvider GetTemplateProvider(string templateProviderName)
        {
            var templateProvider = TemplateProviders
                    .SingleOrDefault(c => c.GetTemplateProviderName().Equals(templateProviderName, StringComparison.OrdinalIgnoreCase))
                ;
            if (templateProvider == null)
                throw new ArgumentOutOfRangeException(nameof(templateProviderName), $"Invalid or unsupported {typeof(ITemplateProvider).GetNonInterfaceName()}: {templateProviderName}");

            return templateProvider;
        }
    }
}
