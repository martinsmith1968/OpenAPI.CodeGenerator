using System;
using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Common.RenderEngines
{
    public abstract class BaseRenderEngine : IRenderEngine
    {
        public virtual string Name => GetType().GetRenderEngineName();

        public virtual string FileExtension => "liquid";

        public virtual void InitialiseIncludes(ITemplateProvider templateProvider, ILanguage language)
        {
        }

        public virtual void RegisterType(Type type)
        {
        }

        public abstract string RenderTemplate(string templateText, object parameters);
    }
}
