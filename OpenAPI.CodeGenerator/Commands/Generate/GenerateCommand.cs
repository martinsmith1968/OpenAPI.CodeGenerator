using System.Collections.Generic;
using System.IO;
using System.Linq;
using DNX.Helpers.Strings;
using Microsoft.OpenApi.Models;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Languages;
using OpenAPI.CodeGenerator.OpenAPI;
using OpenAPI.CodeGenerator.OpenAPI.Items;
using OpenAPI.CodeGenerator.OutputWriters;
using OpenAPI.CodeGenerator.Parser;
using OpenAPI.CodeGenerator.RenderEngines;
using OpenAPI.CodeGenerator.TemplateProviders;

namespace OpenAPI.CodeGenerator.Commands.Generate
{
    public class GenerateCommand : ICommand
    {
        private readonly CommandLineParser _parser;
        private readonly Arguments _arguments;

        private readonly ILanguage _language;
        private readonly IRenderEngine _renderEngine;
        private readonly ITemplateProvider _templateProvider;
        private readonly IOutputWriter _outputWriter;
        private readonly OpenApiDocument _document;

        public GenerateCommand(string[] args)
        {
            _arguments = args.ParseArguments<Arguments>(out _parser);

            _language         = LanguageFactory.GetLanguageAndConfigure(_arguments.Language, _arguments.LanguageOptions);
            _renderEngine     = RenderEngineFactory.GetRenderEngine(_arguments.RenderEngine);
            _templateProvider = TemplateProviderFactory.GetTemplateProvider(_arguments.TemplateProvider);
            _outputWriter     = OutputWriterFactory.GetOutputWriter(_arguments.OutputTarget);
            _document         = OpenApiDocumentFactory.ReadDocumentFromFile(_arguments.OpenApiDocumentFileName);
        }

        public void Execute()
        {
            _renderEngine.InitialiseIncludes(_arguments.TemplateProvider, _templateProvider, _language);

            var internalTypes = typeof(APIController).Assembly.GetTypes()
                .Where(t => (t.FullName ?? string.Empty).StartsWith(typeof(APIController).Namespace))
                .ToList();
            internalTypes.Add(typeof(FileInfo));
            internalTypes.ForEach(t => _renderEngine.RegisterType(t));

            var openApiTypes = typeof(OpenApiDocument).Assembly.GetTypes()
                //.Where(t => t.Name.StartsWith("OpenApi") || t.IsEnum)
                .ToList();
            openApiTypes.ForEach(t => _renderEngine.RegisterType(t));

            var definition = APIDefinition.Create(_arguments.OpenApiDocumentFileName, _document, _arguments.GroupName);

            GenerateDefinition(definition);
        }

        private void GenerateDefinition(APIDefinition definition)
        {
            var definitionName = StringExtensions.CoalesceNullOrEmpty(
                _arguments.OutputLocation,
                _language.SetOutputFileNameExtension(definition.Name, false),
                _language.SetOutputFileNameExtension(_arguments.OpenApiDocumentFileName, true)
            );

            var fileName = _language.BuildOutputFileName(definitionName, TemplateItemType.Definition);
            if (!string.IsNullOrEmpty(fileName))
            {
                _outputWriter.InitialiseFile(fileName);

                var template = _templateProvider.GetTemplate(_renderEngine, _language, TemplateItemType.Definition);

                var output = _renderEngine.RenderTemplate(template, definition);

                _outputWriter.WriteContent(fileName, output);
            }



            //var controller = APIController.Create(definition);
            //
            //GenerateController(controller);
        }

        private void GenerateController(APIController controller)
        {
            var fileName = _language.BuildOutputFileName(controller.Name, TemplateItemType.Controller);
            if (string.IsNullOrEmpty(fileName))
                return;

            _outputWriter.InitialiseFile(fileName);

            var template = _templateProvider.GetTemplate(_renderEngine, _language, TemplateItemType.Controller);

            var output = _renderEngine.RenderTemplate(template, controller);

            _outputWriter.WriteContent(fileName, output);

            var actions = APIAction.Create(controller);

            foreach (var action in actions)
            {
                GenerateAction(action);
            }
        }

        private void GenerateAction(APIAction action)
        {
            var fileName = _language.BuildOutputFileName(action.Name, TemplateItemType.Action);
            if (string.IsNullOrEmpty(fileName))
                return;

            var template = _templateProvider.GetTemplate(_renderEngine, _language, TemplateItemType.Action);

            var output = _renderEngine.RenderTemplate(template, action);

            _outputWriter.WriteContent(fileName, output);
        }
    }
}
