using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Constants;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.TemplateProviders.Implementation
{
    public class FileSystemTemplateProvider : ITemplateProvider
    {
        private readonly IEnumerable<ILanguage> _languages;

        public TemplateProviderType TemplateProviderType => TemplateProviderType.FileSystem;

        public FileSystemTemplateProvider(IEnumerable<ILanguage> languages)
        {
            _languages = languages;
        }

        public IList<string> GetAvailableLanguages()
        {
            var paths = _languages
                .Select(GetBaseLocation)
                .ToList();

            return paths;
        }

        public string GetBaseLocation(ILanguage language)
        {
            var baseFolder = language.GetType().Assembly.Location;

            var baseLocation = Path.Combine(baseFolder, TemplateConstants.DefaultTemplatesFolder);

            return baseLocation;
        }

        public string GetTemplatePath(IRenderEngine renderEngine, ILanguage language)
        {
            var baseLocation = GetBaseLocation(language);

            var path = Path.Combine(baseLocation, language.TemplateFolderName, renderEngine.Name);

            return path;
        }

        public string GetTemplate(IRenderEngine renderEngine, ILanguage language, TemplateItemType templateItemType)
        {
            var fileName = Path.Combine(GetTemplatePath(renderEngine, language), string.Concat(templateItemType, ".", renderEngine.FileExtension));
            if (!DoesTemplateExist(language, fileName))
            {
                fileName = Path.Combine(GetTemplatePath(renderEngine, language), string.Concat("_", templateItemType, ".", renderEngine.FileExtension));
            }

            return File.Exists(fileName)
                ? File.ReadAllText(fileName)
                : null;
        }

        public bool DoesTemplateExist(ILanguage language, string fullTemplateName)
        {
            return File.Exists(fullTemplateName);
        }
    }
}
