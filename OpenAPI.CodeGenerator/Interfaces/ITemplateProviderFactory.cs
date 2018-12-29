using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface ITemplateProviderFactory
    {
        ITemplateProvider GetTemplateProvider(TemplateProviderType templateProviderType);
    }
}
