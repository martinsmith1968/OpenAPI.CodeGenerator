using OpenAPI.CodeGenerator.Common.Languages;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Languages.Implementation
{
    public class CSVLanguage : BaseLanguage
    {
        public override string FileExtension => "csv";

        private string _definitionFileName;

        public override string BuildOutputFileName(string itemName, TemplateItemType templateItemType)
        {
            if (templateItemType == TemplateItemType.Definition)
            {
                _definitionFileName = itemName;
            }

            return _definitionFileName;
        }
    }
}
