using System;
using System.Linq;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Commands;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator
{
    public class ApplicationArguments : IValidatableArguments, IHelpArguments
    {
        [CommandLineArgument(Position = 0, IsRequired = false, DefaultValue = default(CommandType))]
        public CommandType Command { get; set; }

        [Alias("?")]
        [CommandLineArgument(IsRequired = false, DefaultValue = false)]
        public bool Help { get; set; }

        public void Validate()
        {
            var commandNames = Enum.GetNames(typeof(CommandType));

            if (!commandNames.Contains(Command.ToString()))
                throw new CommandLineArgumentException($"Unknown Command - must be one of: {string.Join(",", commandNames)}", CommandLineArgumentErrorCategory.MissingRequiredArgument);
        }
    }
}
