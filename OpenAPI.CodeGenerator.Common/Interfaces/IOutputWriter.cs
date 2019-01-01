using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface IOutputWriter
    {
        OutputTargetType OutputTargetType { get; }

        void InitialiseFile(string fileName);

        void WriteContent(string fileName, string content);
    }
}
