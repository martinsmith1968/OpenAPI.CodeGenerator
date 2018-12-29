using System;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Extensions;
using OpenAPI.CodeGenerator.Interfaces;
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
        private readonly ITemplateProviderFactory _templateProviderFactory;

        private CommandLineParser _parser;
        private Arguments _arguments;
        private ITemplateProvider _templateProvider;

        public ListTemplatesCommand(string[] args)
        {
        }

        public ListTemplatesCommand(ITemplateProviderFactory templateProviderFactory)
        {
            _templateProviderFactory = templateProviderFactory;
        }

        public void SetArguments(string[] args)
        {
            _arguments = args.ParseArguments<Arguments>(out _parser);

            _templateProvider = _templateProviderFactory.GetTemplateProvider(_arguments.TemplateProvider);
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
