using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Factories
{
    public class OutputWriterFactory : IOutputWriterFactory
    {
        public IEnumerable<IOutputWriter> OutputWriters { get; }

        public OutputWriterFactory(IEnumerable<IOutputWriter> outputWriters)
        {
            OutputWriters = outputWriters;
        }

        public IOutputWriter GetOutputWriter(OutputTargetType outputTargetType)
        {
            var outputWriter = OutputWriters
                    .SingleOrDefault(c => c.OutputTargetType.Equals(outputTargetType))
                ;
            if (outputWriter == null)
                throw new ArgumentOutOfRangeException(nameof(outputTargetType), $"Invalid or unsupported {nameof(OutputTargetType)}: {outputTargetType.ToString()}");

            return outputWriter;
        }
    }
}
