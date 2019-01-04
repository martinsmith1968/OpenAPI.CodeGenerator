using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.OpenAPI.Items;

namespace OpenAPI.CodeGenerator.OpenAPI.Extensions
{
    public static class OpenApiDocumentExtensions
    {
        public static IList<OpenApiServer> GetServers(this OpenApiDocument document)
        {
             var servers = (document?.Servers ?? Enumerable.Empty<OpenApiServer>()).ToList();

            return servers;
        }

        public static IList<string> GetSchemes(this OpenApiDocument document)
        {
            return document.GetServers()
                .Select(x => x.GetScheme())
                .ToArray();
        }

        public static string GetHost(this OpenApiDocument document)
        {
            return document.GetServers()
                .Select(s => s.GetHost())
                .FirstOrDefault(s => !string.IsNullOrEmpty(s));
        }

        public static string GetBasePath(this OpenApiDocument document)
        {
            return document.GetServers()
                .Select(s => s.GetBasePath())
                .FirstOrDefault(s => !string.IsNullOrEmpty(s));
        }

        public static IList<APIController> GetControllers(this OpenApiDocument document)
        {
            if (document == null)
                return null;

            var fileTagNames = document.Tags
                .SelectOrDefault(t => t.Name)
                .Where(n => !string.IsNullOrEmpty(n))
                .Distinct()
                .ToArray();

            if (fileTagNames.Any())
            {
                var tagControllers = fileTagNames
                    .Select(t => APIController.Create(t, document.Paths.GetPathsForTag(t)))
                    .ToList();

                return tagControllers;
            }

            var finalPath = document.GetBasePath()
                .Split("/".ToCharArray())
                .Last();

            var pathControllers = new []
            {
                APIController.Create(finalPath, document.Paths)
            };

            return pathControllers;
        }
    }
}
