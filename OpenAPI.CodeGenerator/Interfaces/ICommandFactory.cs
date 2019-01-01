using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface ICommandFactory
    {
        ICommand GetCommandAndConfigure(string commandName, string[] args);

        ICommand GetCommandByName(string commandName);
    }
}
