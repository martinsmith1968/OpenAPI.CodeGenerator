using System.Collections.Generic;
using DotLiquid;

namespace OpenAPI.CodeGenerator.Renderer.DotLiquid
{
    public static class DotLiquidExtensions
    {
        public static Hash ToHash(this object obj)
        {
            return Hash.FromAnonymousObject(obj);
        }

        public static Hash ToHash(this IDictionary<string, object> dictionary)
        {
            return Hash.FromDictionary(dictionary);
        }
    }
}
