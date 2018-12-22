using DotLiquid;
using OpenAPI.CodeGenerator.Configuration;
using OpenAPI.CodeGenerator.Renderers.FileSystemProviders;
using OpenAPI.CodeGenerator.TemplateProviders;

namespace OpenAPI.CodeGenerator.Renderers.Implementation
{
    public class DotLiquidRenderEngine : IRenderEngine
    {
        public string Name => "DotLiquid";

        public void Initialise(TemplateProviderType templateProviderType, ITemplateProvider templateProvider)
        {
            Template.FileSystem = FileSystemProviderFactory.GetFileSystemProvider(templateProviderType, templateProvider.BaseFolder);
        }

        public string RenderTemplate(string templateText, object parameters)
        {
            var template = Template.Parse(templateText);

            var renderParameters = Hash.FromAnonymousObject(parameters);

            var output = template.Render(renderParameters);

            return output;
        }
    }
}
