﻿using System;
using Fluid;
using OpenAPI.CodeGenerator.Common.Constants;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.RenderEngines;
using OpenAPI.CodeGenerator.RenderEngine.Fluid.FileProviders;

namespace OpenAPI.CodeGenerator.RenderEngine.Fluid
{
    public class FluidRenderEngine : BaseRenderEngine
    {
        private readonly TemplateContext _templateContext;

        public FluidRenderEngine()
        {
            _templateContext = new TemplateContext();
        }

        public override void RegisterType(Type type)
        {
            _templateContext.MemberAccessStrategy.Register(type);
        }

        public override void InitialiseIncludes(ITemplateProvider templateProvider, ILanguage language)
        {
            _templateContext.FileProvider = FileProviderFactory.GetFileProvider(templateProvider.TemplateProviderType, templateProvider.GetTemplatePath(this, language));
        }

        public override string RenderTemplate(string templateText, object parameters)
        {
            if (!FluidTemplate.TryParse(templateText, out var template))
                throw new Exception("Unable to parse template");

            _templateContext.SetValue(TemplateConstants.DefinitionName, parameters);

            var output = template.Render(_templateContext);

            return output;
        }
    }
}
