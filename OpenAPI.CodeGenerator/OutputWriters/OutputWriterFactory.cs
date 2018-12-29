using System;
using Autofac;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.OutputWriters
{
    public class OutputWriterFactory : IOutputWriterFactory
    {
        public IOutputWriter GetOutputWriter(OutputTargetType outputTargetType)
        {
            var outputWriter = Program.Container.ResolveOptionalKeyed<IOutputWriter>(outputTargetType);
            if (outputWriter == null)
                throw new ArgumentOutOfRangeException(nameof(outputTargetType), $"Invalid or unsupported {nameof(OutputTargetType)}: {outputTargetType.ToString()}");

            return outputWriter;
        }
    }
}
