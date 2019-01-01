using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Constants;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.TemplateProviders.Implementation
{
    public class ResourceTemplateProvider : ITemplateProvider
    {
        private readonly IEnumerable<ILanguage> _languages;

        public TemplateProviderType TemplateProviderType => TemplateProviderType.Resource;

        public ResourceTemplateProvider(IEnumerable<ILanguage> languages)
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
            var baseNamespace = language.GetType().Namespace;

            var baseLocation = string.Concat(baseNamespace, ".", TemplateConstants.DefaultTemplatesFolder);

            return baseLocation;
        }

        public string GetTemplatePath(IRenderEngine renderEngine, ILanguage language)
        {
            var baseLocation = GetBaseLocation(language);

            var path = Combine(baseLocation, language.TemplateFolderName, renderEngine.Name);

            return path;
        }

        public string GetTemplate(IRenderEngine renderEngine, ILanguage language, TemplateItemType templateItemType)
        {
            var resourceName = Combine(GetTemplatePath(renderEngine, language), templateItemType.ToString(), renderEngine.FileExtension);
            if (!DoesTemplateExist(language, resourceName))
            {
                resourceName = Combine(GetTemplatePath(renderEngine, language), string.Concat("_",  templateItemType.ToString()), renderEngine.FileExtension);
            }

            var assembly = language.GetType().Assembly;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();

                    return result;
                }
            }
        }

        public bool DoesTemplateExist(ILanguage language, string fullTemplateName)
        {
            using (var stream = language.GetType().Assembly.GetManifestResourceStream(fullTemplateName))
            {
                return stream != null;
            }
        }

        private static string Combine(params string[] paths)
        {
            return string.Join(".", paths);
        }
    }
}
