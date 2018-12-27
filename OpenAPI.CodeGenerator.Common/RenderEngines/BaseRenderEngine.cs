using System;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.RenderEngines
{
    public abstract class BaseRenderEngine : IRenderEngine
    {
        public virtual string Name => GetType().Name.Replace(typeof(BaseRenderEngine).Name.Replace("Base", null), null);

        public virtual string FileExtension => "liquid";

        public virtual void InitialiseIncludes(TemplateProviderType templateProviderType, ITemplateProvider templateProvider, string languageFolder)
        {
        }

        public virtual void RegisterType(Type type)
        {
        }

        public abstract string RenderTemplate(string templateText, object parameters);
    }
}
