using System;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.RenderEngine.DotLiquid;
using OpenAPI.CodeGenerator.RenderEngine.Fluid;
using OpenAPI.CodeGenerator.RenderEngine.LiquidNET;
using OpenAPI.CodeGenerator.RenderEngine.Razor;
using OpenAPI.CodeGenerator.RenderEngine.Scriban;

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
