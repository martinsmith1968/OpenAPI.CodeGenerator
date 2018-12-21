using OpenAPI.CodeGenerator.Arguments;
using OpenAPI.CodeGenerator.TemplateProviders;

namespace OpenAPI.CodeGenerator.Renderers
{
    public interface IRenderEngine
    {
        string Name { get; }

        void Initialise(TemplateProviderType templateProviderType, ITemplateProvider templateProvider);

        string RenderTemplate(string templateText, object parameters);
    }
}
