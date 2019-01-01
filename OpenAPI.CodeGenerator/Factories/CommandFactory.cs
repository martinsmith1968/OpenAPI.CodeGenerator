using System;
using System.Collections.Generic;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Factories
{
    public class CommandFactory : ICommandFactory
    {
        public IEnumerable<ICommand> Commands { get; }

        public IDictionary<string, ICommand> CommandsDictionary => Commands
            .ToDictionary(c => c.GetCommandName(), c => c);

        public CommandFactory(IEnumerable<ICommand> commands)
        {
            Commands = commands;
        }

        public ICommand GetCommandAndConfigure(string commandName, string[] args)
        {
            var command = GetCommandByName(commandName);

            command.SetArguments(args);

            return command;
        }

        public ICommand GetCommandByName(string commandName)
        {
            var command = Commands
                .SingleOrDefault(c => string.Equals(c.Name, commandName, StringComparison.OrdinalIgnoreCase))
                ;
            if (command == null)
                throw new ArgumentOutOfRangeException(nameof(commandName), $"Invalid or unsupported {typeof(ICommand).GetNonInterfaceName()}: {commandName}");

            return command;
        }
    }
}
