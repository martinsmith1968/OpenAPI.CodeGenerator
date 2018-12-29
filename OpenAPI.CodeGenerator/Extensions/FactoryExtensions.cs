using System;
using System.Linq;
using System.Reflection;
using DNX.Helpers.Reflection;

namespace OpenAPI.CodeGenerator.Extensions
{
    public static class FactoryExtensions
    {
        public static Type[] FindLoadedTypesThatInheritFrom(this Type type)
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies()
                .Select(Assembly.Load)
                .ToArray();

            var types = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsA(type))
                .ToArray();

            return types;
        }
    }
}
