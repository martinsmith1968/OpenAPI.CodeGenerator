using System.Linq;
using DNX.Helpers.Strings;
using Microsoft.OpenApi.Models;

namespace OpenAPI.CodeGenerator.OpenAPI.Extensions
{
    public static class OpenApiOperationExtensions
    {
        public static string GetControllerName(this OpenApiOperation operation)
        {
            var ownerName = operation.GetOwnerName();

            return $"{ownerName}Controller";
        }

        public static string GetOwnerName(this OpenApiOperation operation)
        {
            var ownerName = StringExtensions.CoalesceNullOrEmpty(
                operation?.Tags?.FirstOrDefault()?.Name,
                operation?.OperationId,
                "Unknown"
            );

            return ownerName;
        }
    }
}
