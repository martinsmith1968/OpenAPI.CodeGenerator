using System;
using System.Linq;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Extensions;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator
{
    public class Application : IApplication
    {
        private readonly ICommandFactory _commandFactory;

        public ApplicationArguments Arguments { get; private set; }

        public CommandLineParser Parser { get; private set; }

        public Application(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public int Execute(string[] args)
        {
            try
            {
                args = (args ?? Enumerable.Empty<string>())
                    .ToArray();

                var programArgs = args.Take(1).ToArray();

                Arguments = programArgs.ParseArguments<ApplicationArguments>(out var parser);
                Parser = parser;

                if (!Parser.IsValid(Arguments))
                {
                    throw new CommandLineArgumentException("Unable to parse arguments", CommandLineArgumentErrorCategory.Unspecified);
                }

                if (Arguments.Help)
                {
                    Parser.ShowHelpToConsole();
                    return 1;
                }

                var command = _commandFactory.GetCommand(Arguments.Command, args.Skip(1).ToArray());

                command.Execute();

                return 0;
            }
            catch (CommandLineArgumentException e)
            {
                if (Parser.IsValid(Arguments) && Arguments.Help)
                {
                    Parser.ShowHelpToConsole();
                    return 2;
                }

                Console.Error.WriteLine($"Error: {e.Message}");

                return 3;
            }
        }
    }
}
