using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface ITemplateProviderFactory
    {
        ITemplateProvider GetTemplateProvider(string templateProviderName);
    }
}
