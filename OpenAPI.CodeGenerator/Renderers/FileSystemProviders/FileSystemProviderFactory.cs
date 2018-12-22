using System.Reflection;
using DotLiquid.FileSystems;
using OpenAPI.CodeGenerator.Configuration;

namespace OpenAPI.CodeGenerator.Renderers.FileSystemProviders
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
                    return new EmbeddedFileSystem(Assembly.GetExecutingAssembly(), baseFolder);

                default:
                    return new BlankFileSystem();
            }
        }
    }
}
