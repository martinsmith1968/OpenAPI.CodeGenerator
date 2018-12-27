using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Commands.Generate
{
    public class Arguments : IHelpArguments
    {
        [CommandLineArgument(Position = 0, IsRequired = true)]
        public string OpenApiDocumentFileName { get; set; }

        [Alias("l")]
        [CommandLineArgument(IsRequired = false, DefaultValue = LanguageType.CSV)]
        public LanguageType Language { get; set; }

        [CommandLineArgument(IsRequired = false)]
        public string LanguageOptions { get; set; }

        [Alias("e")]
        [CommandLineArgument(IsRequired = false, DefaultValue = RenderEngineType.DotLiquid)]
        public RenderEngineType RenderEngine { get; set; }

        [Alias("t")]
        [CommandLineArgument(IsRequired = false, DefaultValue = TemplateProviderType.Resource)]
        public TemplateProviderType TemplateProvider { get; set; }

        [Alias("g")]
        [CommandLineArgument(IsRequired = false)]
        public string GroupName { get; set; }



        [Alias("o")]
        [CommandLineArgument(IsRequired = false)]
        public string OutputLocation { get; set; }

        [Alias("?")]
        [CommandLineArgument(IsRequired = false, DefaultValue = false)]
        public bool Help { get; set; }

        public OutputTargetType OutputTarget => string.IsNullOrEmpty(OutputLocation) ? OutputTargetType.Console : OutputTargetType.Filesystem;

        public string OutputDirectory => System.IO.Path.GetDirectoryName(OutputLocation);

        public void Validate()
        {
        }
    }
}
