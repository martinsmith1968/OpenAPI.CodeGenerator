using System;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Commands;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Commands.ListRenderEngines
{
    public class ListRenderEnginesCommand : BaseCommand
    {
        private readonly IRenderEngineFactory _renderEngineFactory;

        public ListRenderEnginesCommand(IRenderEngineFactory renderEngineFactory)
        {
            _renderEngineFactory = renderEngineFactory;
        }

        public override void Execute()
        {
            var index = 0;
            var renderEngines = _renderEngineFactory.RenderEngines
                .OrderBy(n => n.Name)
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
