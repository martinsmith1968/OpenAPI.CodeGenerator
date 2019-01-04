using DNX.Helpers.Strings;
using Microsoft.OpenApi.Models;
using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.OpenAPI.Extensions;
using StringExtensions = DNX.Helpers.Strings.StringExtensions;

namespace OpenAPI.CodeGenerator.OpenAPI.Items
{
    public class APIAction
    {
        public OperationType OperationType { get; private set; }

        public OpenApiOperation Operation { get; private set; }

        public static APIAction Create(OperationType operationType, OpenApiOperation operation)
        {
            var action = new APIAction()
            {
                OperationType = operationType,
                Operation = operation
            };

            return action;
        }

        public string GetOperationName(string suffix = "")
        {
            var operationName = StringExtensions.CoalesceNullOrEmpty(
                Operation?.OperationId.UpperCaseFirstLetter(),
                string.Concat(OperationType.ToString().UpperCaseFirstLetter(), Operation.GetMethodName(null)),
                "Unknown"
            )
                .EnsureEndsWith(suffix);

            return operationName;
        }
    }
}
