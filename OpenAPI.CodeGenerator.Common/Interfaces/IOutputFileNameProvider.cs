using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface IOutputFileNameProvider
    {
        string GetOutputFileName(TemplateItemType templateItemType, string itemName);
    }
}
