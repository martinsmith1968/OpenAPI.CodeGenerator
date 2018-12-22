using System;
using OpenAPI.CodeGenerator.Configuration;
using OpenAPI.CodeGenerator.Renderers.Implementation;

namespace OpenAPI.CodeGenerator.Renderers
{
    public class RenderEngineFactory
    {
        public static IRenderEngine GetRenderer(RenderEngineType rendererType)
        {
            switch (rendererType)
            {
                case RenderEngineType.DotLiquid:
                    return new DotLiquidRenderEngine();

                default:
                    throw new ArgumentOutOfRangeException(nameof(rendererType), $"Invalid or unsupported {nameof(RenderEngineType)}: {rendererType.ToString()}");
            }
        }
    }
}
