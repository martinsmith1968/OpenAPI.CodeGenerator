using System.Reflection;
using DotLiquid.FileSystems;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.RenderEngine.DotLiquid.FileSystemProviders
{
    public class FileSystemProviderFactory
    {
        public static IFileSystem GetFileSystemProvider(TemplateProviderType templateProviderType, string baseFolder)
        {
            switch (templateProviderType)
            {
                case TemplateProviderType.FileSystem:
                    return new LocalFileSystem(baseFolder);

                case TemplateProviderType.Resource:
                    return new EmbeddedFileSystem(Assembly.GetEntryAssembly(), baseFolder);

                default:
                    return new BlankFileSystem();
            }
        }
    }
}
