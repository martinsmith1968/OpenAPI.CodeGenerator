using System;
using System.Collections.Generic;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Factories
{
    public class RenderEngineFactory : IRenderEngineFactory
    {
        public IEnumerable<IRenderEngine> RenderEngines { get; }

        public RenderEngineFactory(IEnumerable<IRenderEngine> renderEngines)
        {
            RenderEngines = renderEngines;
        }

        public IRenderEngine GetRenderEngineByName(string renderEngineName)
        {
            var renderEngine = RenderEngines
                    .SingleOrDefault(c => c.GetRenderEngineName().Equals(renderEngineName, StringComparison.OrdinalIgnoreCase))
                ;
            if (renderEngine == null)
                throw new ArgumentOutOfRangeException(nameof(renderEngineName), $"Invalid or unsupported {nameof(IRenderEngine)}: {renderEngineName}");

            return renderEngine;
        }
    }
}
