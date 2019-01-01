using System;
using Autofac;
using OpenAPI.CodeGenerator.Interfaces;
using OpenAPI.CodeGenerator.IoC;

namespace OpenAPI.CodeGenerator
{
    internal class Program
    {
        private static IContainer Container { get; set; }

        private static IApplication _application;

        private static int Main(string[] args)
        {
            try
            {
                Container = AutofacIocRegistrations.BuildContainer();

                _application = Container.Resolve<IApplication>();
                _application.Execute(args);

                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"{e.GetType().Name} Error: {e.Message}");

                return 4;
            }
            finally
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    while (Console.KeyAvailable)
                        Console.ReadKey(true);

                    Console.Write("Press any key to exit...");
                    Console.ReadKey(true);
                    Console.WriteLine();
                }
            }
        }
    }
}
