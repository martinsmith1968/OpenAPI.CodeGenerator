using System;
using System.Linq;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Commands;
using OpenAPI.CodeGenerator.Parser;

namespace OpenAPI.CodeGenerator
{
    internal class Program
    {
        private static ProgramArguments _programArguments;
        private static CommandLineParser _parser;

        private static int Main(string[] args)
        {
            try
            {
                var programArgs = (args ?? Enumerable.Empty<string>()).Take(1).ToArray();

                _programArguments = programArgs.ParseArguments<ProgramArguments>(out _parser);
                if (!_parser.IsValid(_programArguments))
                {
                    throw new CommandLineArgumentException("Unable to parse arguments", CommandLineArgumentErrorCategory.Unspecified);
                }

                if (_programArguments.Help)
                {
                    _parser.ShowHelpToConsole();
                    return 1;
                }

                var command = CommandFactory.GetCommand(_programArguments.Command, args.Skip(1).ToArray());

                command.Execute();

                return 0;
            }
            catch (CommandLineArgumentException e)
            {
                if (_parser.IsValid(_programArguments) && _programArguments.Help)
                {
                    _parser.ShowHelpToConsole();
                    return 2;
                }

                Console.Error.WriteLine($"Error: {e.Message}");

                return 3;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"{e.GetType().Name} Error: {e.Message}");

                return 4;
            }
            finally
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    while (Console.KeyAvailable)
                        Console.ReadKey(true);

                    Console.Write("Press any key to exit...");
                    Console.ReadKey(true);
                    Console.WriteLine();
                }
            }
        }
    }
}
