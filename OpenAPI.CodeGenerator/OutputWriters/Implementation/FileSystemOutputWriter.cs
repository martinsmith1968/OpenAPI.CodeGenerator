using System.IO;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.OutputWriters.Implementation
{
    public class FileSystemOutputWriter : IOutputWriter
    {
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
