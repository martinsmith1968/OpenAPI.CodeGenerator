using System;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Commands.ListRenderEngines
{
    public class ListRenderEnginesCommand : ICommand
    {
        public ListRenderEnginesCommand(string[] args)
        {
        }

        public void Execute()
        {
            var renderEngines = Enum.GetNames(typeof(RenderEngineType))
                                    .OrderBy(n => n)
                                    .ToArray();

            var indexSize = renderEngines.Count().ToString().Length;

            Console.WriteLine("Render Engines:");

            var index = 0;
            foreach (var renderEngine in renderEngines)
            {
                Console.WriteLine(string.Format("{0," + indexSize + "}: {1}", ++index, renderEngine));
            }
        }
    }
}
