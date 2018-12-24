using System;
using Liquid.NET;
using Liquid.NET.Constants;
using Liquid.NET.Utils;
using OpenAPI.CodeGenerator.Common.BaseImplementation;

namespace OpenAPI.CodeGenerator.LiquidNETRenderer
{
    public class LiquidNETRenderEngine : BaseRenderEngine
    {
        private ITemplateContext _templateContext;

        public LiquidNETRenderEngine()
        {
            _templateContext = new TemplateContext();
        }

        public override string RenderTemplate(string templateText, object parameters)
        {
            var liquidParameters = parameters.ToLiquid();

            // Any values that you want to put into the TemplateContext in Liquid.NET
            // are put into wrappers.  For example, the call to the static factory
            // LiquidString.Create(String value) will create a string-wrapper of the
            // type LiquidString.

            _templateContext.DefineLocalVariable("definition", parameters.ToLiquid());

            // You can generate a template with "LiquidTemplate.Create(String template)".
            // Values that are rendered are wrapped in double-parentheses: "{{" and "}}".
            var template = LiquidTemplate.Create(templateText);

            var output = template.LiquidTemplate.Render(_templateContext).Result;

            return output;
        }
    }
}
