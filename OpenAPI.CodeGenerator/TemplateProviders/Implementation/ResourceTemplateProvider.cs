using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.TemplateProviders.Implementation
{
    public class ResourceTemplateProvider : ITemplateProvider
    {
        public static string DefaultResourceTemplateFolder = string.Concat(typeof(Program).Namespace, ".", "Templates");
        private readonly Assembly _templateAssembly;

        public string BaseLocation => DefaultResourceTemplateFolder;

        public ResourceTemplateProvider()
        {
            _templateAssembly = Assembly.GetEntryAssembly();
        }

        public IList<string> GetAvailableLanguages()
        {
            var resources = _templateAssembly.GetManifestResourceNames()
                    .Where(x => x.StartsWith(BaseLocation));

            var paths = resources
                .Select(x => x.Remove(0, BaseLocation.Length + 1))
                .Select(x => x.Split(".".ToCharArray()).FirstOrDefault())
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .ToList();

            return paths;
        }

        public string GetTemplatePath(IRenderEngine renderEngine, ILanguage language)
        {
            throw new System.NotImplementedException();
        }

        public string GetTemplate(IRenderEngine renderEngine, ILanguage language, TemplateItemType templateItemType)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesTemplateExist(IRenderEngine renderEngine, ILanguage language, string templateName)
        {
            throw new System.NotImplementedException();
        }

        public string GetTemplatePath(IRenderEngine renderEngine, string language)
        {
            var path = Combine(BaseLocation, language, renderEngine.Name);

            return path;
        }

        public string GetTemplate(IRenderEngine renderEngine, string languageFolder, TemplateItemType templateItemType)
        {
            var resourceName = Combine(GetTemplatePath(renderEngine, languageFolder), templateItemType.ToString(), renderEngine.FileExtension);
            if (!DoesTemplateExist(resourceName))
            {
                resourceName = Combine(GetTemplatePath(renderEngine, languageFolder), string.Concat("_",  templateItemType.ToString()), renderEngine.FileExtension);
            }

            using (var stream = _templateAssembly.GetManifestResourceStream(resourceName))
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

        public bool DoesTemplateExist(string fullTemplateName)
        {
            using (var stream = _templateAssembly.GetManifestResourceStream(fullTemplateName))
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
