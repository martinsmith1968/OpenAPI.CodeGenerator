using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenAPI.CodeGenerator.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static T FirstOrEmpty<T>(this IEnumerable<T> enumerable)
            where T : class, new()
        {
            var empty = new T();

            return enumerable?.FirstOrDefault()
                   ?? empty;
        }

        public static IList<TV> SelectOrDefault<T, TV>(this IList<T> enumerable, Func<T, TV> func)
        {
            if (enumerable == null)
                return new List<TV>();

            return enumerable
                .Select(func)
                .ToList();
        }
    }
}
