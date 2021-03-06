﻿using System.Linq;
using Microsoft.OpenApi.Models;
using OpenAPI.CodeGenerator.OpenAPI.Extensions;

namespace OpenAPI.CodeGenerator.OpenAPI.Items
{
    public class APIController
    {
        public string Name { get; private set; }

        public OpenApiPaths OpenApiPathItem { get; private set; }

        private APIController()
        {
        }

        public static APIController Create(APIDefinition apiDefinition)
        {
            return new APIController
            {
                Name            = FindBestControllerName(apiDefinition),
                OpenApiPathItem = apiDefinition.OpenApiDocument.Paths
            };
        }

        private static string FindBestControllerName(APIDefinition apiDefinition)
        {
            var bestName = DNX.Helpers.Strings.StringExtensions.CoalesceNullOrEmpty(
                apiDefinition.OpenApiDocument?.Tags?.FirstOrDefault()?.Reference?.Id,
                GetLastPathName(apiDefinition.OpenApiDocument?.GetBasePath())
            );

            var controllerName = string.IsNullOrEmpty(bestName)
                ? bestName
                : string.Concat(bestName.Substring(0, 1).ToUpper(), bestName.Substring(1));

            return controllerName;
        }

        private static string GetLastPathName(string path)
        {
            return (path ?? string.Empty).Split("/".ToCharArray())
                .LastOrDefault();
        }
    }
}
