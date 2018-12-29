using OpenAPI.CodeGenerator.Commands;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface ICommandFactory
    {
        ICommand GetCommand(CommandType commandType, string[] args);
    }
}
