using Microsoft.OpenApi.Models;

namespace OpenAPI.CodeGenerator.OpenAPI.Items
{
    public class APIAction
    {
        public string Name { get; set; }

        public OpenApiPathItem OpenApiPathItem { get; set; }
    }
}
