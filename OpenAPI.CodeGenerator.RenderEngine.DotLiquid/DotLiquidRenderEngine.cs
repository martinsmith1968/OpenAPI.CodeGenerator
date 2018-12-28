using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotLiquid;
using OpenAPI.CodeGenerator.Common.Constants;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.RenderEngines;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.RenderEngine.DotLiquid.FileSystemProviders;

namespace OpenAPI.CodeGenerator.RenderEngine.DotLiquid
{
    public class DotLiquidRenderEngine : BaseRenderEngine
    {
        public override void InitialiseIncludes(TemplateProviderType templateProviderType, ITemplateProvider templateProvider, ILanguage language)
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

            var dictionary = new Dictionary<string, object>
            {
                { TemplateConstants.DefinitionName, Hash.FromAnonymousObject(parameters, true) }
            };

#if SHIT
            var renderParameters = Hash.FromDictionary(dictionary);
#else
            var renderParameters = Hash.FromAnonymousObject(parameters);
#endif

            var output = template.Render(renderParameters);

            return output;
        }
    }
}
