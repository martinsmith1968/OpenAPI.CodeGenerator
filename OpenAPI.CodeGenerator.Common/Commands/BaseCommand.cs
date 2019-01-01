using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Common.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public virtual string Name => GetType().GetCommandName();

        public virtual void SetArguments(string[] args)
        {
        }

        public abstract void Execute();
    }
}
