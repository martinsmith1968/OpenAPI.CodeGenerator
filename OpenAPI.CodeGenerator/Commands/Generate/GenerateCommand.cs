using System.IO;
using System.Linq;
using DNX.Helpers.Strings;
using Microsoft.OpenApi.Models;
using Ookii.CommandLine;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.OpenAPI;
using OpenAPI.CodeGenerator.OpenAPI.Items;
using OpenAPI.CodeGenerator.OutputFileNames;
using OpenAPI.CodeGenerator.OutputWriters;
using OpenAPI.CodeGenerator.Parser;
using OpenAPI.CodeGenerator.Renderers;
using OpenAPI.CodeGenerator.TemplateProviders;

namespace OpenAPI.CodeGenerator.Commands.Generate
{
    public class GenerateCommand : ICommand
    {
        private readonly CommandLineParser _parser;
        private readonly Arguments _arguments;

        private readonly OpenApiDocument _document;
        private readonly IRenderEngine _renderEngine;
        private readonly ITemplateProvider _templateProvider;
        private readonly IOutputFileNameProvider _outputFileNameProvider;
        private readonly IOutputWriter _outputWriter;

        public GenerateCommand(string[] args)
        {
            _arguments = args.ParseArguments<Arguments>(out _parser);

            _document               = OpenApiDocumentFactory.ReadDocumentFromFile(_arguments.OpenApiDocumentFileName);
            _renderEngine           = RenderEngineFactory.GetRenderEngine(_arguments.RenderEngine);
            _templateProvider       = TemplateProviderFactory.GetTemplateProvider(_arguments.TemplateProvider);
            _outputFileNameProvider = OutputFileNameProviderFactory.GetOutputFileProvider(_arguments.OutputType, _arguments.OutputPath);
            _outputWriter           = OutputWriterFactory.GetOutputWriter(_arguments.OutputTarget);
        }

        public void Execute()
        {
            _renderEngine.InitialiseIncludes(_arguments.TemplateProvider, _templateProvider, _arguments.Language);

            var internalTypes = typeof(APIController).Assembly.GetTypes()
                .Where(t => (t.FullName ?? string.Empty).StartsWith(typeof(APIController).Namespace))
                .ToList();
            internalTypes.ForEach(t => _renderEngine.RegisterType(t));

            var openApiTypes = typeof(OpenApiDocument).Assembly.GetTypes()
                .Where(t => t.Name.StartsWith("OpenApi") || t.IsEnum)
                .ToList();
            openApiTypes.ForEach(t => _renderEngine.RegisterType(t));

            var definition = APIDefinition.Create(_document);

            GenerateDefinition(definition);
        }

        private void GenerateDefinition(APIDefinition definition)
        {
            var controller = APIController.Create(definition);

            GenerateController(controller);
        }

        private void GenerateController(APIController controller)
        {
            var fileName = _outputFileNameProvider.GetOutputFileName(TemplateItemType.Controller, controller.Name);
            if (string.IsNullOrEmpty(fileName))
                return;

            _outputWriter.InitialiseFile(fileName);

            var template = _templateProvider.GetTemplate(_renderEngine, _arguments.Language, TemplateItemType.Controller);

            var output = _renderEngine.RenderTemplate(template, controller);

            _outputWriter.WriteContent(fileName, output);

            foreach (var action in controller.OpenApiPathItem)
            {
                GenerateAction(action.Key, action.Value);
            }
        }

        private void GenerateAction(string actionLocation, OpenApiPathItem actionItem)
        {

        }
    }
}
