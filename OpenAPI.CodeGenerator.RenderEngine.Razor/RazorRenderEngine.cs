using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.RenderEngines;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace OpenAPI.CodeGenerator.RenderEngine.Razor
{
    public class RazorRenderEngine : BaseRenderEngine
    {
        public override string FileExtension => "cshtml";

        public override void InitialiseIncludes(ITemplateProvider templateProvider, ILanguage language)
        {
            var config = new TemplateServiceConfiguration();
            config.CachingProvider = new DefaultCachingProvider(t =>
            {
                // TODO
            });

            var service = RazorEngineService.Create(config);

            Engine.Razor = service;
        }

        public override string RenderTemplate(string templateText, object parameters)
        {
            var output = Engine.Razor.RunCompile(templateText, templateText.GetHashCode().ToString(), parameters.GetType(), parameters);

            return output;
        }
    }
}
