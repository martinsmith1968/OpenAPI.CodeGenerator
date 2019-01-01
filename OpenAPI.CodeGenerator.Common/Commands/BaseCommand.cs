using DNX.Helpers.Strings;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Common.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public virtual void SetArguments(string[] args)
        {
        }

        public abstract void Execute();
    }
}
