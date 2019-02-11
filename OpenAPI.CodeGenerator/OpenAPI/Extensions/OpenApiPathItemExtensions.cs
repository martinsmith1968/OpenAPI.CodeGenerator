using System.Collections.Generic;
using System.Linq;
using DNX.Helpers.Strings;
using Microsoft.OpenApi.Models;
using OpenAPI.CodeGenerator.OpenAPI.Items;

namespace OpenAPI.CodeGenerator.OpenAPI.Extensions
{
    public static class OpenApiPathItemExtensions
    {
        public static IList<APIAction> GetActions(this OpenApiPathItem pathItem)
        {
            var actions = pathItem?.Operations
                .Select(o => APIAction.Create(o.Key, o.Value))
                .ToList();

            return actions;
        }

        public static string GetControllerName(this OpenApiPathItem pathItem, string path)
        {
            var name = StringExtensions.CoalesceNullOrEmpty(
                path.RemoveStartsAndEndsWith("/").Trim(),
                pathItem.Description,
                pathItem.Summary
            );

            return name;
        }
    }
}
