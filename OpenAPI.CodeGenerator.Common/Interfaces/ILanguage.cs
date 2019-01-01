using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface ILanguage
    {
        string Name { get; }

        string FileExtension { get; }

        string TemplateFolderName { get; }

        void ApplyArguments(string[] args);

        string SetOutputFileNameExtension(string fileName, bool containsExtension);

        string BuildOutputFileName(string itemName, TemplateItemType templateItemType);
    }
}
