using System.Linq;
using System.Reflection;
using Autofac;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Factories;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.IoC
{
    public static class AutofacIocRegistrations
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder()
                    .RegisterInternal()
                    .RegisterBuiltInFactories()
                    .RegisterImplementableTypes()
                ;

            var container = builder.Build();

            return container;
        }

        private static ContainerBuilder RegisterInternal(this ContainerBuilder builder)
        {
            builder.RegisterType<Application>().As<IApplication>();

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

        private static ContainerBuilder RegisterImplementableTypes(this ContainerBuilder builder)
        {
            var assemblies = new[]
                {
                    Assembly.GetEntryAssembly(),
                    Assembly.GetExecutingAssembly()
                }
                .Distinct()
                .ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(ICommand).IsAssignableFrom(t))
                .InstancePerDependency()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(ILanguage).IsAssignableFrom(t))
                .InstancePerDependency()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(IOutputWriter).IsAssignableFrom(t))
                .InstancePerDependency()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(ITemplateProvider).IsAssignableFrom(t))
                .InstancePerDependency()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(IRenderEngine).IsAssignableFrom(t))
                .InstancePerDependency()
                .AsImplementedInterfaces();

            return builder;
        }
    }
}
