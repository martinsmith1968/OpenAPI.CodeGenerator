using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator.TemplateProviders
{
    public class ResourceTemplateProvider : ITemplateProvider
    {
        public static string DefaultResourceTemplateFolder = string.Concat(typeof(Program).Namespace, ".", "Templates");
        public static string DefaultResourceTemplateExtension = "template";

        public string BaseFolder { get; }
        public string FileExtension { get; }

        public ResourceTemplateProvider(string languageFolder)
            : this(languageFolder, DefaultResourceTemplateFolder, DefaultResourceTemplateExtension)
        {
        }

        public ResourceTemplateProvider(string languageFolder, string baseFolder, string fileExtension)
        {
            BaseFolder = string.IsNullOrEmpty(languageFolder)
                ? baseFolder
                : string.Concat(baseFolder, ".", languageFolder);
            FileExtension = fileExtension;
        }

        public IList<string> GetInstalledLanguages()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resources = assembly.GetManifestResourceNames()
                    .Where(x => x.StartsWith(BaseFolder));

            var paths = resources
                .Select(x => x.Remove(0, BaseFolder.Length + 1))
                .Select(x => x.Split(".".ToCharArray()).FirstOrDefault())
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .ToList();

            return paths;
        }

        public string GetTemplate(TemplateItemType templateItemType)
        {
            var resourceName = string.Concat(BaseFolder, ".", templateItemType, ".", FileExtension);

            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();

                    return result;
                }
            }
        }
    }
}
