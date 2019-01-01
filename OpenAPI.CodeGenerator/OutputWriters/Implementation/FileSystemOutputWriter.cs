using System.IO;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.OutputWriters.Implementation
{
    public class FileSystemOutputWriter : IOutputWriter
    {
        public OutputTargetType OutputTargetType => OutputTargetType.FileSystem;

        public void InitialiseFile(string fileName)
        {
            EnsureDirectoryExists(Path.GetDirectoryName(fileName));

            File.WriteAllText(fileName, null);
        }

        public void WriteContent(string fileName, string content)
        {
            File.AppendAllText(fileName, content);
        }

        private static void EnsureDirectoryExists(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
                return;

            Directory.CreateDirectory(directoryName);
        }
    }
}
