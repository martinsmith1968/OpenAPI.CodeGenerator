namespace OpenAPI.CodeGenerator.Commands
{
    public interface ICommand
    {
        void SetArguments(string[] args);

        void Execute();
    }
}
