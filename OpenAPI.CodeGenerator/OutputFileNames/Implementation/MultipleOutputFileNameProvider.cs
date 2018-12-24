using System.IO;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

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
