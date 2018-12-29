using System;
using Autofac;
using OpenAPI.CodeGenerator.Extensions;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Commands
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCommand(CommandType commandType, string[] args)
        {
            var command = Program.Container.ResolveOptionalKeyed<ICommand>(commandType);
            if (command == null)
                throw new ArgumentOutOfRangeException(nameof(commandType), $"Invalid or unsupported {nameof(CommandType)}: {commandType.ToString()}");

            command.SetArguments(args);

            return command;
        }
    }
}
