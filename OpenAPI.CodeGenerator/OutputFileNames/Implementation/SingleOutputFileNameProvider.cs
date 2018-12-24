using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.OutputFileNames.Implementation
{
    public class SingleOutputFileNameProvider : IOutputFileNameProvider
    {
        public string OutputPath { get; }

        public SingleOutputFileNameProvider(string outputPath)
        {
            OutputPath = outputPath;
        }

        public string GetOutputFileName(TemplateItemType itemType, string itemName)
        {
            return OutputPath;
        }
    }
}
