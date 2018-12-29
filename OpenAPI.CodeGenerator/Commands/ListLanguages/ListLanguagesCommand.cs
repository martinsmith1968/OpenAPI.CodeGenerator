using System;
using System.Linq;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Parser;
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
        private readonly CommandLineParser _parser;
        private readonly Arguments _arguments;
        private readonly ITemplateProvider _templateProvider;

        public ListLanguagesCommand(string[] args)
        {
            _arguments = args.ParseArguments<Arguments>(out _parser);

            _templateProvider = TemplateProviderFactory.GetTemplateProvider(_arguments.TemplateProvider);
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
