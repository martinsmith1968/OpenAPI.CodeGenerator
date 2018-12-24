using System;
using System.Linq;
using System.Reflection;
using DotLiquid;
using OpenAPI.CodeGenerator.Common.BaseImplementation;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.DotLiquidRenderer.FileSystemProviders;

namespace OpenAPI.CodeGenerator.DotLiquidRenderer
{
    public class DotLiquidRenderEngine : BaseRenderEngine
    {
        public override void InitialiseIncludes(TemplateProviderType templateProviderType, ITemplateProvider templateProvider, string language)
        {
            Template.FileSystem = FileSystemProviderFactory.GetFileSystemProvider(templateProviderType, templateProvider.GetTemplatePath(this, language));
        }

        public override void RegisterType(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Public);

            var propertyNames = properties
                .Select(p => p.Name)
                .ToArray();

            Template.RegisterSafeType(type, propertyNames);
        }

        public override string RenderTemplate(string templateText, object parameters)
        {
            var template = Template.Parse(templateText);

            var renderParameters = Hash.FromAnonymousObject(parameters, true);

            var output = template.Render(renderParameters);

            return output;
        }
    }
}
