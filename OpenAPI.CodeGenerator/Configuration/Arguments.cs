using Ookii.CommandLine;

namespace OpenAPI.CodeGenerator.Configuration
{
    public class Arguments
    {
        [CommandLineArgument(Position = 0, IsRequired = true)]
        public string OpenApiDocumentFileName { get; set; }

        [Alias("o")]
        [CommandLineArgument(IsRequired = false, DefaultValue = ".")]
        public string OutputPath { get; set; }

        [CommandLineArgument(IsRequired = false, DefaultValue = "csharp")]
        public string Language { get; set; }

        [CommandLineArgument(IsRequired = false, DefaultValue = OutputType.SingleFile)]
        public OutputType OutputType { get; set; }






        [CommandLineArgument(IsRequired = false, DefaultValue = TemplateProviderType.Resource)]
        public TemplateProviderType TemplateProvider { get; set; }

        [CommandLineArgument(IsRequired = false, DefaultValue = RenderEngineType.DotLiquid)]
        public RenderEngineType RenderEngine { get; set; }




        [Alias("list")]
        [CommandLineArgument(IsRequired = false, DefaultValue = false)]
        public bool ListInstalledLanguages { get; set; }


        [Alias("?")]
        [CommandLineArgument(IsRequired = false, DefaultValue = false)]
        public bool Help { get; set; }

        public string OutputDirectory => System.IO.Path.GetDirectoryName(OutputPath);

        public void Validate()
        {
        }
    }
}
