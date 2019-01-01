using Ookii.CommandLine;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface IApplication
    {
        int Execute(string[] args);

        CommandLineParser Parser { get; }
    }
}
