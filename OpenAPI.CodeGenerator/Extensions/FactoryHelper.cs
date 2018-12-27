using System;
using System.Linq;
using System.Reflection;
using DNX.Helpers.Reflection;

namespace OpenAPI.CodeGenerator.Extensions
{
    public class FactoryHelper
    {
        public static Type[] FindLoadedTypes(Type type)
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
