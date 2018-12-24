using Microsoft.OpenApi.Models;

namespace OpenAPI.CodeGenerator.OpenAPI.Items
{
    public class APIDefinition
    {
        public string Title => DNX.Helpers.Strings.StringExtensions.CoalesceNullOrEmpty(
            OpenApiDocument?.Info?.Title
        );

        public OpenApiDocument OpenApiDocument { get; set; }

        public static APIDefinition Create(OpenApiDocument document)
        {
            return new APIDefinition
            {
                OpenApiDocument = document
            };
        }
    }
}
