using System;
using System.Linq;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Commands;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Extensions;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Commands.ListLanguages
{
    public class Arguments
    {
        [CommandLineArgument(IsRequired = true)]
        public string TemplateProvider { get; set; }
    }

    public class ListLanguagesCommand : BaseCommand
    {
        private readonly ITemplateProviderFactory _templateProviderFactory;

        private CommandLineParser _parser;
        private Arguments _arguments;
        private ITemplateProvider _templateProvider;

        public ListLanguagesCommand(ITemplateProviderFactory templateProviderFactory)
        {
            _templateProviderFactory = templateProviderFactory;
        }

        public override void SetArguments(string[] args)
        {
            _arguments = args.ParseArguments<Arguments>(out _parser);

            _templateProvider = _templateProviderFactory.GetTemplateProvider(_arguments.TemplateProvider);
        }

        public override void Execute()
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
