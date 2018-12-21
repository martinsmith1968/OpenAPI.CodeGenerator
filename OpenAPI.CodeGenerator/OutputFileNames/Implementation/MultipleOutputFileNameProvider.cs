using System.IO;
using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator.OutputFileNames.Implementation
{
    public class MultipleOutputFileNameProvider : IOutputFileNameProvider
    {
        public string OutputPath { get; }

        public MultipleOutputFileNameProvider(string outputPath)
        {
            OutputPath = Path.GetDirectoryName(outputPath);
        }

        public string GetOutputFileName(TemplateItemType itemType, string itemName)
        {
            return Path.Combine(OutputPath, itemType.ToString(), itemName);
        }
    }
}
