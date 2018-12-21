using System;
using Ookii.CommandLine;

namespace OpenAPI.CodeGenerator
{
    internal class Program
    {
        protected internal static CommandLineParser Parser;
        protected internal static Arguments.Arguments Arguments;

        protected internal static bool CanShowHelp => Parser != null && Arguments != null;
        protected internal static bool IsHelpRequested => Arguments != null && Arguments.Help;

        private static int Main(string[] args)
        {
            try
            {
                Parser = new CommandLineParser(typeof(Arguments.Arguments));
                Arguments = (Arguments.Arguments)Parser.Parse(args);

                if (CanShowHelp && IsHelpRequested)
                {
                    ShowHelp();
                    return 1;
                }

                Arguments.Validate();

                var application = new Application(Arguments);

                if (Arguments.ListInstalledLanguages)
                {
                    var languages = application.GetInstalledLanguages();

                    Console.WriteLine("Languages:");
                    foreach(var language in languages)
                        Console.WriteLine($"  {language}");
                }
                else
                {
                    application.GenerateOutputFiles();

                }

                return 0;
            }
            catch (CommandLineArgumentException e)
            {
                if (CanShowHelp && IsHelpRequested)
                {
                    ShowHelp();
                    return 2;
                }

                Console.Error.WriteLine($"{e.GetType().Name} Error: {e.Message}");

                return 3;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"{e.GetType().Name} Error: {e.Message}");

                return 4;
            }
        }

        private static void ShowHelp()
        {
            if (Arguments != null && Arguments.Help)
            {
                var options = new WriteUsageOptions()
                {
                };

                Parser.WriteUsageToConsole(options);
            }
        }
    }
}
