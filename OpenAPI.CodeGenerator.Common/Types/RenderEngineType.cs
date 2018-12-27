namespace OpenAPI.CodeGenerator.Common.Types
{
    public enum RenderEngineType
    {
        DotLiquid,

        // Fails to parse OpenApiDocument.ToLiquid() - suspect because of recursive properties
        //LiquidNET,

        Fluid,

        Scriban,

        Razor
    }
}
