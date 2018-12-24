using System;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.DotLiquidRenderer;
using OpenAPI.CodeGenerator.Fluid;
using OpenAPI.CodeGenerator.LiquidNETRenderer;

namespace OpenAPI.CodeGenerator.Renderers
{
    public class RenderEngineFactory
    {
        public static IRenderEngine GetRenderEngine(RenderEngineType rendererType)
        {
            switch (rendererType)
            {
                case RenderEngineType.DotLiquid:
                    return new DotLiquidRenderEngine();

                case RenderEngineType.LiquidNET:
                    return new LiquidNETRenderEngine();

                case RenderEngineType.Fluid:
                    return new FluidRenderEngine();

                default:
                    throw new ArgumentOutOfRangeException(nameof(rendererType), $"Invalid or unsupported {nameof(RenderEngineType)}: {rendererType.ToString()}");
            }
        }
    }
}
