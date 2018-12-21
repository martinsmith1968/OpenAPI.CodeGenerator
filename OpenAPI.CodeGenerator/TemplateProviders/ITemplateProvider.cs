using System.Collections.Generic;
using OpenAPI.CodeGenerator.Templates;

namespace OpenAPI.CodeGenerator.TemplateProviders
{
    public interface ITemplateProvider
    {
        string BaseFolder { get; }
        string FileExtension { get; }

        IList<string> GetInstalledLanguages();

        string GetTemplate(TemplateItemType templateItemType);
    }
}
