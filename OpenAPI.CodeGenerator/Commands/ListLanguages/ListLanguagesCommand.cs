using System;
using System.Linq;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Extensions;
using OpenAPI.CodeGenerator.Interfaces;
using OpenAPI.CodeGenerator.TemplateProviders;

namespace OpenAPI.CodeGenerator.Commands.ListLanguages
{
    public class Arguments
    {
        [CommandLineArgument(IsRequired = false, DefaultValue = TemplateProviderType.Resource)]
        public TemplateProviderType TemplateProvider { get; set; }
    }

    public class ListLanguagesCommand : ICommand
    {
        private readonly ITemplateProviderFactory _templateProviderFactory;

        private CommandLineParser _parser;
        private Arguments _arguments;
        private ITemplateProvider _templateProvider;

        public ListLanguagesCommand(string[] args)
        {
        }

        public ListLanguagesCommand(ITemplateProviderFactory templateProviderFactory)
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
            var index = 0;
            var languages = _templateProvider.GetAvailableLanguages()
                .OrderBy(n => n)
                .ToDictionary(x => ++index, x => x);

            var indexSize = languages.Count().ToString().Length;

            Console.WriteLine("Languages:");
            foreach (var language in languages)
            {
                Console.WriteLine(string.Format("{0," + indexSize + "}: {1}", language.Key, language.Value));
            }
        }
    }
}
