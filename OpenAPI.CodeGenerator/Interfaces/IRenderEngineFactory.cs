using System.Collections.Generic;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface IRenderEngineFactory
    {
        IRenderEngine GetRenderEngineByName(string rendererEngineName);
        IEnumerable<IRenderEngine> RenderEngines { get; }
    }
}
