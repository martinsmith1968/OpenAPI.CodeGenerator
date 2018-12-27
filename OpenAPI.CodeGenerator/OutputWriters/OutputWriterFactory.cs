using System;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.OutputWriters.Implementation;

namespace OpenAPI.CodeGenerator.OutputWriters
{
    public class OutputWriterFactory
    {
        public static IOutputWriter GetOutputWriter(OutputTargetType outputTargetType)
        {
            switch (outputTargetType)
            {
                case OutputTargetType.Console:
                    return new ConsoleOutputWriter();

                case OutputTargetType.Filesystem:
                    return new FileSystemOutputWriter();

                default:
                    throw new ArgumentOutOfRangeException(nameof(outputTargetType), $"Invalid or unsupported {nameof(OutputTargetType)}: {outputTargetType.ToString()}");
            }
        }
    }
}
