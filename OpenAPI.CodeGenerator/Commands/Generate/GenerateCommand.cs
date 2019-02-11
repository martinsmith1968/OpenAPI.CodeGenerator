using System.IO;
using System.Linq;
using DNX.Helpers.Strings;
using Microsoft.OpenApi.Models;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Commands;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Extensions;
using OpenAPI.CodeGenerator.Interfaces;
using OpenAPI.CodeGenerator.OpenAPI;
using OpenAPI.CodeGenerator.OpenAPI.Extensions;
using OpenAPI.CodeGenerator.OpenAPI.Items;

namespace OpenAPI.CodeGenerator.Commands.Generate
{
    public class GenerateCommand : BaseCommand
    {
        private readonly ILanguageFactory _languageFactory;
        private readonly IOutputWriterFactory _outputWriterFactory;
        private readonly IRenderEngineFactory _renderEngineFactory;
        private readonly ITemplateProviderFactory _templateProviderFactory;

        private CommandLineParser _parser;
        private Arguments _arguments;

        private ILanguage _language;
        private IRenderEngine _renderEngine;
        private ITemplateProvider _templateProvider;
        private IOutputWriter _outputWriter;
        private OpenApiDocument _document;

        public GenerateCommand(
            ILanguageFactory languageFactory,
            IOutputWriterFactory outputWriterFactory,
            IRenderEngineFactory renderEngineFactory,
            ITemplateProviderFactory templateProviderFactory
            )
        {
            _languageFactory         = languageFactory;
            _outputWriterFactory     = outputWriterFactory;
            _renderEngineFactory     = renderEngineFactory;
            _templateProviderFactory = templateProviderFactory;
        }

        public override void SetArguments(string[] args)
        {
            _arguments = args.ParseArguments<Arguments>(out _parser);

            _language         = _languageFactory.GetLanguageAndConfigure(_arguments.Language, _arguments.LanguageOptions);
            _renderEngine     = _renderEngineFactory.GetRenderEngineByName(_arguments.RenderEngine);
            _templateProvider = _templateProviderFactory.GetTemplateProvider(_arguments.TemplateProvider);
            _outputWriter     = _outputWriterFactory.GetOutputWriter(_arguments.OutputTarget);

            _document = OpenApiDocumentFactory.ReadDocumentFromFile(_arguments.OpenApiDocumentFileName);
        }

        public override void Execute()
        {
            _renderEngine.InitialiseIncludes(_templateProvider, _language);

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
                _language.GetOutputFileName(definition.Name, false),
                _language.GetOutputFileName(_arguments.OpenApiDocumentFileName, true)
            );

            var fileName = _language.BuildOutputFileName(definitionName, TemplateItemType.Definition);
            if (!string.IsNullOrEmpty(fileName))
            {
                _outputWriter.InitialiseFile(fileName);

                var template = _templateProvider.GetTemplate(_renderEngine, _language, TemplateItemType.Definition);

                var output = _renderEngine.RenderTemplate(template, definition);

                _outputWriter.WriteContent(fileName, output);
            }

            return;

            foreach (var controller in definition.OpenApiDocument.GetControllers())
            {
                GenerateController(controller);
            }
        }

        private void GenerateController(APIController controller)
        {
            var fileName = _language.BuildOutputFileName(controller.Name, TemplateItemType.Controller);
            if (!string.IsNullOrEmpty(fileName))
            {
                _outputWriter.InitialiseFile(fileName);

                var template = _templateProvider.GetTemplate(_renderEngine, _language, TemplateItemType.Controller);

                var output = _renderEngine.RenderTemplate(template, controller);

                _outputWriter.WriteContent(fileName, output);
            }

            foreach (var path in controller.OpenApiPaths)
            {
                foreach (var action in path.Value.GetActions())
                {
                    GenerateAction(action);
                }
            }
        }

        private void GenerateAction(APIAction action)
        {
            var fileName = _language.BuildOutputFileName(action.GetOperationName(), TemplateItemType.Action);
            if (string.IsNullOrEmpty(fileName))
                return;

            var template = _templateProvider.GetTemplate(_renderEngine, _language, TemplateItemType.Action);

            var output = _renderEngine.RenderTemplate(template, action);

            _outputWriter.WriteContent(fileName, output);
        }
    }
}
