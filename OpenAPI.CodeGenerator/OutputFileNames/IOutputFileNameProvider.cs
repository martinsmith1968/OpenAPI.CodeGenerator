using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator.OutputFileNames
{
    public interface IOutputFileNameProvider
    {
        string GetOutputFileName(TemplateItemType templateItemType, string itemName);
    }
}
