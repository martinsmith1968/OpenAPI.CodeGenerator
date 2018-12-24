namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface IOutputWriter
    {
        void InitialiseFile(string fileName);

        void WriteContent(string fileName, string content);
    }
}
