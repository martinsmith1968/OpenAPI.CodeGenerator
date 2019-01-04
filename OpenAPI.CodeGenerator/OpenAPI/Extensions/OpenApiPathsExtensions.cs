using System.Linq;
using Microsoft.OpenApi.Models;

namespace OpenAPI.CodeGenerator.OpenAPI.Extensions
{
    public static class OpenApiPathsExtensions
    {
        public static OpenApiPaths GetPathsForTag(this OpenApiPaths paths, string tagName)
        {
            if (paths == null)
                return paths;

            var matchingPaths = paths
                .Where(p => p.Value.Operations.Values.Any(o => o.Tags.Any(t => t.Name.Equals(tagName))))
                .ToList();

            var pathsForTag = new OpenApiPaths();
            matchingPaths.ForEach(mp => pathsForTag.Add(mp.Key, mp.Value));
            return pathsForTag;
        }
    }
}
