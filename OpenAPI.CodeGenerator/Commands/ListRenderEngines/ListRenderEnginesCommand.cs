using System;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Commands.ListRenderEngines
{
    public class ListRenderEnginesCommand : ICommand
    {
        public ListRenderEnginesCommand()
        {
        }

        public void SetArguments(string[] args)
        {
        }

        public void Execute()
        {
            var index = 0;
            var renderEngines = Enum.GetNames(typeof(RenderEngineType))
                .OrderBy(n => n)
                .ToDictionary(x => ++index, x => x);

            var indexSize = renderEngines.Count().ToString().Length;

            Console.WriteLine("Render Engines:");
            foreach (var renderEngine in renderEngines)
            {
                Console.WriteLine(string.Format("{0," + indexSize + "}: {1}", renderEngine.Key, renderEngine.Value));
            }
        }
    }
}
