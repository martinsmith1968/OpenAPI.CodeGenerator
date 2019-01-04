using DNX.Helpers.Strings;
using Microsoft.OpenApi.Models;
using OpenAPI.CodeGenerator.Common.Extensions;
using StringExtensions = DNX.Helpers.Strings.StringExtensions;

namespace OpenAPI.CodeGenerator.OpenAPI.Extensions
{
    public static class OpenApiOperationExtensions
    {
        public static string GetMethodName(this OpenApiOperation operation, string suffix = "")
        {
            var methodName = operation.GetOperationName()
                .EnsureEndsWith(suffix);

            return methodName;
        }

        public static string GetOperationName(this OpenApiOperation operation)
        {
            var operationName = StringExtensions.CoalesceNullOrEmpty(
                operation?.OperationId,
                string.Join("", operation?.Tags?.SelectOrDefault(t => t.Name.UpperCaseFirstLetter())),
                "Unknown"
            );

            return operationName;
        }
    }
}
