using DotLiquid;

namespace OpenAPI.CodeGenerator.Renderers.Implementation
{
    public class DotLiquidRenderEngine : IRenderEngine
    {
        public string RenderTemplate(string templateText, object parameters)
        {
            var template = Template.Parse(templateText);

            var renderParameters = Hash.FromAnonymousObject(parameters);

            var output = template.Render(renderParameters);

            return output;
        }
    }
}
