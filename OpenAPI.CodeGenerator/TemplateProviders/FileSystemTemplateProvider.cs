using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator.TemplateProviders
{
    public class FileSystemTemplateProvider : ITemplateProvider
    {
        public static string DefaultFileSystemTemplateFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Templates");
        public static string DefaultFileSystemTemplateExtension = "template";

        public string BaseFolder { get; }
        public string FileExtension { get; }

        public FileSystemTemplateProvider(string languageFolder)
            :this(languageFolder, DefaultFileSystemTemplateFolder, DefaultFileSystemTemplateExtension)
        {
        }

        public FileSystemTemplateProvider(string languageFolder, string baseFolder, string fileExtension)
        {
            BaseFolder = string.IsNullOrEmpty(languageFolder)
                ? baseFolder
                : Path.Combine(baseFolder, languageFolder);
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
