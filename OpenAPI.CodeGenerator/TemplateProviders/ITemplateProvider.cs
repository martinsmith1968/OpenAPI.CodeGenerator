using System.Collections.Generic;
using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator.TemplateProviders
{
    public interface ITemplateProvider
    {
        IList<string> GetInstalledLanguages();

        string GetTemplate(TemplateItemType templateItemType);
    }
}
