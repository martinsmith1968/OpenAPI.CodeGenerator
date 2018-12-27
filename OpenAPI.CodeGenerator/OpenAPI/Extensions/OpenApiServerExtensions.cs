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
            var url = server?.Url ?? string.Empty;

            var uri = ParseUrl(url);

            if (uri == null)
            {
                url = url.Replace("///", "//localhost/");

                uri = ParseUrl(url);
            }

            if (uri == null)
            {
                uri = new Uri(url);
            }

            return uri;
        }

        private static Uri ParseUrl(string url)
        {
            try
            {
                var uri = new Uri(url);

                return uri;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);

                return null;
            }
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
