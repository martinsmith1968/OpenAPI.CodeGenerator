using System.Collections.Generic;
using System.IO;
using Microsoft.OpenApi.Models;
using OpenAPI.CodeGenerator.Configuration;
using OpenAPI.CodeGenerator.OpenAPI;
using OpenAPI.CodeGenerator.OpenAPI.Items;
using OpenAPI.CodeGenerator.OutputFileNames;
using OpenAPI.CodeGenerator.Renderers;
using OpenAPI.CodeGenerator.TemplateProviders;
using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator
{
    public class Application
    {
        protected internal readonly OpenApiDocument Document;
        protected internal readonly IRenderEngine RenderEngine;
        protected internal readonly ITemplateProvider TemplateProvider;
        protected internal readonly IOutputFileNameProvider OutputFileProvider;

        public Arguments Arguments { get; }

        public Application(Arguments arguments)
        {
            Arguments          = arguments;
            Document           = OpenApiDocumentFactory.ReadDocumentFromFile(arguments.OpenApiDocumentFileName);
            RenderEngine       = RenderEngineFactory.GetRenderer(arguments.RenderEngine);
            TemplateProvider   = TemplateProviderFactory.GetTemplateProvider(arguments.TemplateProvider, RenderEngine.Name, arguments.Language);
            OutputFileProvider = OutputFileNameProviderFactory.GetOutputFileProvider(arguments.OutputType, arguments.OutputPath);
        }

        public IList<string> GetInstalledLanguages()
        {
            var languages = TemplateProvider.GetInstalledLanguages();

            return languages;
        }

        public void GenerateOutputFiles()
        {
            RenderEngine.Initialise(Arguments.TemplateProvider, TemplateProvider);

            foreach (var path in Document.Paths)
            {
                GeneratePath(path.Key, path.Value);
            }



        }

        private void GeneratePath(string pathLocation, OpenApiPathItem pathItem)
        {
            var pathName = pathLocation.Trim("/".ToCharArray());

            var fileName = OutputFileProvider.GetOutputFileName(TemplateItemType.Controller, pathName);

            var template = TemplateProvider.GetTemplate(TemplateItemType.Controller);

            var controller = new Controller()
            {
                Name = pathName,
                OpenApiPathItem = pathItem
            };

            var output = RenderEngine.RenderTemplate(template, controller);

            File.AppendAllText(fileName, output);
        }
    }
}
