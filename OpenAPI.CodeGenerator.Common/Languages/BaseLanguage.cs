using System.IO;
using DNX.Helpers.Strings;
using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.Languages
{
    public abstract class BaseLanguage : ILanguage
    {
        public virtual string Name => GetType().GetLanguageName();

        public abstract string FileExtension { get; }

        public virtual string TemplateFolderName => GetType().GetLanguageName().ToLower();

        public virtual void ApplyArguments(string[] args)
        {
        }

        public virtual string GetOutputFileName(string fileLocation, bool containsExtension)
        {
            return containsExtension
                ? Path.ChangeExtension(Path.GetFileNameWithoutExtension(fileLocation), FileExtension)
                : Path.ChangeExtension(fileLocation, FileExtension);
        }

        public abstract string BuildOutputFileName(string itemName, TemplateItemType templateItemType);

        protected static string Pluralize(string text)
        {
            return text.EnsureEndsWith("s");
        }
    }
}
