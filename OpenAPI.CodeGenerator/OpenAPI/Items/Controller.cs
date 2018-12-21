using Microsoft.OpenApi.Models;

namespace OpenAPI.CodeGenerator.OpenAPI.Items
{
    public class Controller
    {
        public string Name { get; set; }

        public OpenApiPathItem OpenApiPathItem { get; set; }
    }
}
