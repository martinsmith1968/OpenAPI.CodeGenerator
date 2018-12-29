using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface IRenderEngineFactory
    {
        IRenderEngine GetRenderEngine(RenderEngineType rendererType);
    }
}
