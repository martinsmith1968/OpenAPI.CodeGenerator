using System;
using DNX.Helpers.Strings;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Common.Extensions
{
    public static class RenderEngineExtensions
    {
        public static string GetRenderEngineName(this Type type)
        {
            return type == null
                ? null
                : type.Name.RemoveEndsWith(nameof(IRenderEngine).RemoveStartsWith("I"));
        }

        public static string GetRenderEngineName(this IRenderEngine renderEngine)
        {
            return renderEngine?.GetType().GetRenderEngineName();
        }
    }
}
