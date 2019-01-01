namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface ICommand
    {
        void SetArguments(string[] args);

        void Execute();
    }
}
