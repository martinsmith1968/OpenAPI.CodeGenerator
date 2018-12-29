using Autofac;
using Autofac.Builder;
using OpenAPI.CodeGenerator.Commands;
using OpenAPI.CodeGenerator.Commands.Generate;
using OpenAPI.CodeGenerator.Commands.ListRenderEngines;
using OpenAPI.CodeGenerator.Commands.ListTemplates;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Interfaces;
using OpenAPI.CodeGenerator.Language.CSharp;
using OpenAPI.CodeGenerator.Language.CSV;
using OpenAPI.CodeGenerator.Languages;
using OpenAPI.CodeGenerator.OutputWriters;
using OpenAPI.CodeGenerator.OutputWriters.Implementation;
using OpenAPI.CodeGenerator.RenderEngine.DotLiquid;
using OpenAPI.CodeGenerator.RenderEngine.Fluid;
using OpenAPI.CodeGenerator.RenderEngine.Razor;
using OpenAPI.CodeGenerator.RenderEngine.Scriban;
using OpenAPI.CodeGenerator.RenderEngines;
using OpenAPI.CodeGenerator.TemplateProviders;
using OpenAPI.CodeGenerator.TemplateProviders.Implementation;

namespace OpenAPI.CodeGenerator.IoC
{
    public static class AutofacIocRegistrations
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder()
                    .RegisterInternal()
                    .RegisterBuiltInFactories()
                    .RegisterBuiltInTypes()
                ;

            var container = builder.Build();

            return container;
        }

        private static ContainerBuilder RegisterInternal(this ContainerBuilder builder)
        {

            return builder;
        }

        private static ContainerBuilder RegisterBuiltInFactories(this ContainerBuilder builder)
        {
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<LanguageFactory>().As<ILanguageFactory>();
            builder.RegisterType<OutputWriterFactory>().As<IOutputWriterFactory>();
            builder.RegisterType<RenderEngineFactory>().As<IRenderEngineFactory>();
            builder.RegisterType<TemplateProviderFactory>().As<ITemplateProviderFactory>();

            return builder;
        }

        private static ContainerBuilder RegisterBuiltInTypes(this ContainerBuilder builder)
        {
            builder.RegisterType<Application>().As<IApplication>();

            builder.RegisterType<GenerateCommand>().Keyed<ICommand>(CommandType.Generate);
            builder.RegisterType<ListTemplatesCommand>().Keyed<ICommand>(CommandType.ListTemplates);
            builder.RegisterType<ListRenderEnginesCommand>().Keyed<ICommand>(CommandType.ListRengerEngines);
            //builder.RegisterType<ICommand>().Keyed<GenerateCommand>(CommandType.ExportEmbeddedTemplates);

            builder.RegisterType<CSharpLanguage>().Keyed<ILanguage>(LanguageType.csharp);
            builder.RegisterType<CSVLanguage>().Keyed<ILanguage>(LanguageType.CSV);

            builder.RegisterType<ConsoleOutputWriter>().Keyed<IOutputWriter>(OutputTargetType.Console);
            builder.RegisterType<FileSystemOutputWriter>().Keyed<IOutputWriter>(OutputTargetType.Filesystem);

            builder.RegisterType<ResourceTemplateProvider>().Keyed<ITemplateProvider>(TemplateProviderType.Resource);
            builder.RegisterType<FileSystemTemplateProvider>().Keyed<ITemplateProvider>(TemplateProviderType.FileSystem);

            builder.RegisterType<DotLiquidRenderEngine>().Keyed<IRenderEngine>(RenderEngineType.DotLiquid);
            //builder.RegisterType<LiquidNETRenderEngine>().Keyed<IRenderEngine>(RenderEngineType.LiquidNET);
            builder.RegisterType<FluidRenderEngine>().Keyed<IRenderEngine>(RenderEngineType.Fluid);
            builder.RegisterType<ScribanRenderEngine>().Keyed<IRenderEngine>(RenderEngineType.Scriban);
            builder.RegisterType<RazorRenderEngine>().Keyed<IRenderEngine>(RenderEngineType.Razor);

            return builder;
        }
    }
}
