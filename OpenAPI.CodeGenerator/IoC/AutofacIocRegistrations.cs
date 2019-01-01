using System.Collections.Generic;
using System.IO;
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
                    .RegisterImplementableTypes()
                    .RegisterInternal()
                    .RegisterBuiltInFactories()
                ;

            var container = builder.Build();

            return container;
        }

        private static IList<Assembly> BuildAssemblyList()
        {
            var fileLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var externalAssemblyFileNames = Directory.GetFiles(fileLocation, "*.dll");

            var baseAssemblies = new[]
                {
                    Assembly.GetEntryAssembly(),
                    Assembly.GetExecutingAssembly(),
                    Assembly.GetCallingAssembly(),
                }
                .Distinct()
                .ToArray();

            var baseReferencedAssemblies = baseAssemblies
                .SelectMany(a => a.GetReferencedAssemblies())
                .Distinct()
                .Select(Assembly.Load)
                .ToArray();

            var externalAssemblies = externalAssemblyFileNames
                .Select(Assembly.LoadFile)
                .ToArray();

            var assemblies = baseReferencedAssemblies
                .Union(baseAssemblies)
                .Union(externalAssemblies)
                .Distinct()
                .ToArray();

            return assemblies;
        }

        private static ContainerBuilder RegisterInternal(this ContainerBuilder builder)
        {
            builder.RegisterType<Application>().As<IApplication>();

            return builder;
        }

        private static ContainerBuilder RegisterBuiltInFactories(this ContainerBuilder builder)
        {
            builder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();
            builder.RegisterType<LanguageFactory>().As<ILanguageFactory>().SingleInstance();
            builder.RegisterType<OutputWriterFactory>().As<IOutputWriterFactory>().SingleInstance();
            builder.RegisterType<RenderEngineFactory>().As<IRenderEngineFactory>().SingleInstance();
            builder.RegisterType<TemplateProviderFactory>().As<ITemplateProviderFactory>().SingleInstance();

            return builder;
        }

        private static ContainerBuilder RegisterImplementableTypes(this ContainerBuilder builder)
        {
            var assemblies = BuildAssemblyList()
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
