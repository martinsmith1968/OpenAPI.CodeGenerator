using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace OpenAPI.CodeGenerator.OpenAPI.Extensions
{
    public static class OpenApiServerExtensions
    {
        public static Uri GetUri(this OpenApiServer server)
        {
            var uri = new Uri(server?.Url ?? string.Empty);

            return uri;
        }

        public static string GetScheme(this OpenApiServer server)
        {
            return server.GetUri().Scheme;
        }

        public static string GetHost(this OpenApiServer server)
        {
            return server.GetUri().Host;
        }

        public static string GetBasePath(this OpenApiServer server)
        {
            return server.GetUri().LocalPath;
        }

        public static IList<string> GetBasePathParts(this OpenApiServer server)
        {
            return server.GetBasePath()
                .Split("/".ToCharArray())
                .ToArray();
        }
    }
}
