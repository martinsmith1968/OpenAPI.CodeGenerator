using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Parser
{
    public static class ParserExtensions
    {
        public static T ParseArguments<T>(this string[] args, out CommandLineParser parser)
        {
            parser = new CommandLineParser(typeof(T));

            var arguments = (T)parser.Parse(args);

            if (arguments is IValidatableArguments validatableArguments)
                validatableArguments.Validate();

            return arguments;
        }

        public static bool IsValid(this CommandLineParser parser)
        {
            return parser != null;
        }

        public static bool IsValid<T>(this CommandLineParser parser, T arguments)
        {
            return parser.IsValid()
                   && arguments != null;
        }

        public static void ShowHelpToConsole(this CommandLineParser parser, WriteUsageOptions options = null)
        {
            options = options ?? new WriteUsageOptions()
            {
                IncludeAliasInDescription = true,
                IncludeApplicationDescription = true,
                IncludeDefaultValueInDescription = true,
            };

            parser.WriteUsageToConsole(options);
        }
    }
}
