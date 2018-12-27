using System;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Renderer.DotLiquid;
using OpenAPI.CodeGenerator.Renderer.Fluid;
using OpenAPI.CodeGenerator.Renderer.Razor;
using OpenAPI.CodeGenerator.Renderer.Scriban;

namespace OpenAPI.CodeGenerator.RenderEngines
{
    public class RenderEngineFactory
    {
        public static IRenderEngine GetRenderEngine(RenderEngineType rendererType)
        {
            switch (rendererType)
            {
                case RenderEngineType.DotLiquid:
                    return new DotLiquidRenderEngine();

                //case RenderEngineType.LiquidNET:
                //    return new LiquidNETRenderEngine();

                case RenderEngineType.Fluid:
                    return new FluidRenderEngine();

                case RenderEngineType.Scriban:
                    return new ScribanRenderEngine();

                case RenderEngineType.Razor:
                    return new RazorRenderEngine();

                default:
                    throw new ArgumentOutOfRangeException(nameof(rendererType), $"Invalid or unsupported {nameof(RenderEngineType)}: {rendererType.ToString()}");
            }
        }
    }
}
