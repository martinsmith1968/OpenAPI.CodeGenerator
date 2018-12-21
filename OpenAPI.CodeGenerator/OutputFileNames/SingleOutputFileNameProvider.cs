using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator.OutputFileNames
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
