using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator.TemplateProviders.Implementation
{
    public class FileSystemTemplateProvider : ITemplateProvider
    {
        public static string DefaultFileSystemTemplateFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Templates");
        public static string DefaultFileSystemTemplateExtension = "template";

        public string BaseFolder { get; }
        public string FileExtension { get; }

        public FileSystemTemplateProvider(string renderEngineName, string languageFolder)
            :this(renderEngineName, languageFolder, DefaultFileSystemTemplateFolder, DefaultFileSystemTemplateExtension)
        {
        }

        public FileSystemTemplateProvider(string renderEngineName, string languageFolder, string baseFolder, string fileExtension)
        {
            BaseFolder = Path.Combine(baseFolder, renderEngineName, languageFolder);
            FileExtension = fileExtension;
        }

        public IList<string> GetInstalledLanguages()
        {
            var paths = Directory.Exists(BaseFolder)
                ? Directory.GetDirectories(BaseFolder)
                : Enumerable.Empty<string>().ToArray();

            return paths;
        }

        public string GetTemplate(TemplateItemType templateItemType)
        {
            var fileName = Path.Combine(BaseFolder, string.Concat(templateItemType, ".", FileExtension));

            return File.Exists(fileName)
                ? File.ReadAllText(fileName)
                : null;
        }
    }
}
