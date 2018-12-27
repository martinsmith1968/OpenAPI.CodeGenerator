using System;
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
            var renderEngines = Enum.GetNames(typeof(RenderEngineType));

            Console.WriteLine("Render Engines:");
            foreach (var renderEngine in renderEngines)
            {
                Console.WriteLine($"  {renderEngine}");
            }
        }
    }
}
