using Microsoft.Extensions.FileProviders;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.RenderEngine.Fluid.FileProviders
{
    public class FileProviderFactory
    {
        public static IFileProvider GetFileProvider(TemplateProviderType templateProviderType, string baseFolder)
        {
            switch (templateProviderType)
            {
                //case TemplateProviderType.FileSystem:
                //    return null;

                default:
                    return new NullFileProvider();
            }
        }
    }
}
