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
                    .SingleOrDefault(c => string.Equals(c.Name, renderEngineName, StringComparison.OrdinalIgnoreCase))
                ;
            if (renderEngine == null)
                throw new ArgumentOutOfRangeException(nameof(renderEngineName), $"Invalid or unsupported {typeof(IRenderEngine).GetNonInterfaceName()}: {renderEngineName}");

            return renderEngine;
        }
    }
}
