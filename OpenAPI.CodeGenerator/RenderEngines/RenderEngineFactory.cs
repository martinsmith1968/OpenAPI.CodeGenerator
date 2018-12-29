using System;
using Autofac;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.RenderEngines
{
    public class RenderEngineFactory : IRenderEngineFactory
    {
        public IRenderEngine GetRenderEngine(RenderEngineType rendererType)
        {
            var renderEngine = Program.Container.ResolveOptionalKeyed<IRenderEngine>(rendererType);
            if (renderEngine == null)
                throw new ArgumentOutOfRangeException(nameof(rendererType), $"Invalid or unsupported {nameof(RenderEngineType)}: {rendererType.ToString()}");

            return renderEngine;
        }
    }
}
