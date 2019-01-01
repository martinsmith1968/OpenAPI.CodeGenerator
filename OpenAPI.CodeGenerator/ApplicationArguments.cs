using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator
{
    public class ApplicationArguments : IValidatableArguments, IHelpArguments
    {
        [CommandLineArgument(Position = 0, IsRequired = true)]
        public string CommandName { get; set; }

        [Alias("?")]
        [CommandLineArgument(IsRequired = false, DefaultValue = false)]
        public bool Help { get; set; }

        public void Validate()
        {
            //if (!commandNames.Contains(Command.ToString()))
            //    throw new CommandLineArgumentException($"Unknown Command - must be one of: {string.Join(",", commandNames)}", CommandLineArgumentErrorCategory.MissingRequiredArgument);
        }
    }
}
