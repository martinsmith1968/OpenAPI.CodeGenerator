using System;
using OpenAPI.CodeGenerator.Arguments;
using OpenAPI.CodeGenerator.OutputFileNames.Implementation;

namespace OpenAPI.CodeGenerator.OutputFileNames
{
    public class OutputFileNameProviderFactory
    {
        public static IOutputFileNameProvider GetOutputFileProvider(OutputType outputType, string outputPath)
        {
            switch (outputType)
            {
                case OutputType.SingleFile:
                    return new SingleOutputFileNameProvider(outputPath);

                case OutputType.MultipleFiles:
                    return new MultipleOutputFileNameProvider(outputPath);

                default:
                    throw new ArgumentOutOfRangeException(nameof(outputType), $"Invalid or unsupported {nameof(OutputType)}: {outputType.ToString()}");
            }
        }
    }
}
