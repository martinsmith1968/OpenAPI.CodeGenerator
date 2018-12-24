using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.TemplateProviders.Implementation
{
    public class FileSystemTemplateProvider : ITemplateProvider
    {
        public static string DefaultFileSystemTemplateFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Templates");

        public string BaseLocation { get; }

        public FileSystemTemplateProvider()
            :this(DefaultFileSystemTemplateFolder)
        {
        }

        public FileSystemTemplateProvider(string baseFolder)
        {
            BaseLocation = baseFolder;
        }

        public IList<string> GetAvailableLanguages()
        {
            var paths = Directory.Exists(BaseLocation)
                ? Directory.GetDirectories(BaseLocation)
                : Enumerable.Empty<string>().ToArray();

            return paths;
        }

        public string GetTemplatePath(IRenderEngine renderEngine, string language)
        {
            var path = Path.Combine(BaseLocation, language, renderEngine.Name);

            return path;
        }

        public string GetTemplate(IRenderEngine renderEngine, string language, TemplateItemType templateItemType)
        {
            var fileName = Path.Combine(GetTemplatePath(renderEngine, language), string.Concat(templateItemType, ".", renderEngine.FileExtension));
            if (!DoesTemplateExist(fileName))
            {
                fileName = Path.Combine(GetTemplatePath(renderEngine, language), string.Concat("_", templateItemType, ".", renderEngine.FileExtension));
            }

            return File.Exists(fileName)
                ? File.ReadAllText(fileName)
                : null;
        }

        public bool DoesTemplateExist(string fullTemplateName)
        {
            return File.Exists(fullTemplateName);
        }
    }
}
