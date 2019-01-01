using System;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.OutputWriters.Implementation
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public OutputTargetType OutputTargetType => OutputTargetType.Console;

        public void InitialiseFile(string fileName)
        {
            WriteContent(fileName, $"File: {fileName}");
        }

        public void WriteContent(string fileName, string content)
        {
            Console.Out.WriteLine(content);
        }
    }
}
