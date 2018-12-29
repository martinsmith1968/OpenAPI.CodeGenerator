using System;
<<<<<<< HEAD
using OpenAPI.CodeGenerator.Commands.Generate;
using OpenAPI.CodeGenerator.Commands.ListRenderEngines;
using OpenAPI.CodeGenerator.Commands.ListLanguages;
=======
using Autofac;
using OpenAPI.CodeGenerator.Extensions;
using OpenAPI.CodeGenerator.Interfaces;
>>>>>>> Factored for manual Ioc resolution

namespace OpenAPI.CodeGenerator.Commands
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCommand(CommandType commandType, string[] args)
        {
            var command = Program.Container.ResolveOptionalKeyed<ICommand>(commandType);
            if (command == null)
                throw new ArgumentOutOfRangeException(nameof(commandType), $"Invalid or unsupported {nameof(CommandType)}: {commandType.ToString()}");

<<<<<<< HEAD
                case CommandType.ListLanguages:
                    return new ListLanguagesCommand(args);

                case CommandType.ListRenderEngines:
                    return new ListRenderEnginesCommand(args);

                default:
                    throw new ArgumentOutOfRangeException(nameof(commandType), $"Invalid or unsupported {nameof(CommandType)}: {commandType.ToString()}");
            }
=======
            command.SetArguments(args);

            return command;
>>>>>>> Factored for manual Ioc resolution
        }
    }
}
