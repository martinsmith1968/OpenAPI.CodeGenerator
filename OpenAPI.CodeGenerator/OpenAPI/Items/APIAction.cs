using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace OpenAPI.CodeGenerator.OpenAPI.Items
{
    public class APIAction
    {
        public string Name { get; set; }

        public OpenApiPathItem OpenApiPathItem { get; set; }

        public static IList<APIAction> Create(APIController controller)
        {
            var actions = controller.OpenApiPathItem
                .Select(x => Create(x.Key, x.Value))
                .ToArray();

            return actions;
        }

        private static APIAction Create(string name, OpenApiPathItem pathItem)
        {
            return new APIAction()
            {
                Name            = name,
                OpenApiPathItem = pathItem
            };
        }
    }
}
