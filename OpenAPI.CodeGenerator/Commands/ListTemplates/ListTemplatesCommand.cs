using System;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Parser;
using OpenAPI.CodeGenerator.TemplateProviders;

namespace OpenAPI.CodeGenerator.Commands.ListTemplates
{
    public class Arguments
    {
        [CommandLineArgument(IsRequired = false, DefaultValue = TemplateProviderType.Resource)]
        public TemplateProviderType TemplateProvider { get; set; }
    }

    public class ListTemplatesCommand : ICommand
    {
        private readonly CommandLineParser _parser;
        private readonly Arguments _arguments;
        private readonly ITemplateProvider _templateProvider;

        public ListTemplatesCommand(string[] args)
        {
            _arguments = args.ParseArguments<Arguments>(out _parser);

            _templateProvider = TemplateProviderFactory.GetTemplateProvider(_arguments.TemplateProvider);
        }

        public void Execute()
        {
            var languages = _templateProvider.GetAvailableLanguages();

            Console.WriteLine("Languages:");
            foreach (var language in languages)
            {
                Console.WriteLine($"  {language}");
            }
        }
    }
}
