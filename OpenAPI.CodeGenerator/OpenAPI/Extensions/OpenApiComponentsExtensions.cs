using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace OpenAPI.CodeGenerator.OpenAPI.Extensions
{
    public static class OpenApiComponentsExtensions
    {
        public static IDictionary<string, OpenApiSchema> GetSchemas(this OpenApiComponents components)
        {
            return components?.Schemas ?? new Dictionary<string, OpenApiSchema>();
        }
    }
}
