using OpenAPI.CodeGenerator.Common.RenderEngines;
using Scriban;

namespace OpenAPI.CodeGenerator.Renderer.Scriban
{
    public class ScribanRenderEngine : BaseRenderEngine
    {
        public override string RenderTemplate(string templateText, object parameters)
        {
            var template = Template.ParseLiquid(templateText);

            var output = template.Render(new { definition = parameters });

            return output;
        }
    }
}
