using System;
using OpenAPI.CodeGenerator.Commands.Generate;
using OpenAPI.CodeGenerator.Commands.ListTemplates;

namespace OpenAPI.CodeGenerator.Commands
{
    public class CommandFactory
    {
        public static ICommand GetCommand(CommandType commandType, string[] args)
        {
            switch (commandType)
            {
                case CommandType.Generate:
                    return new GenerateCommand(args);

                case CommandType.ListTemplates:
                    return new ListTemplatesCommand(args);

                default:
                    throw new ArgumentOutOfRangeException(nameof(commandType), $"Invalid or unsupported {nameof(CommandType)}: {commandType.ToString()}");
            }
        }
    }
}
