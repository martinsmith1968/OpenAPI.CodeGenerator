using System;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.OutputWriters.Implementation
{
    public class ConsoleOutputWriter : IOutputWriter
    {
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
