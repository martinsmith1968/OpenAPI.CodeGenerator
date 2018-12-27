using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;

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
    }
}
