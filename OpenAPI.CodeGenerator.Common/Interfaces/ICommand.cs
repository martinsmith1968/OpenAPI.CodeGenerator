namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        void SetArguments(string[] args);

        void Execute();
    }
}
