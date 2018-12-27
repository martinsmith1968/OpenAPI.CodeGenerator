using System.Collections.Generic;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface ITemplateProvider
    {
        string BaseLocation { get; }

        IList<string> GetAvailableLanguages();

        string GetTemplatePath(IRenderEngine renderEngine, string language);

        string GetTemplate(IRenderEngine renderEngine, string languageFolder, TemplateItemType templateItemType);

        bool DoesTemplateExist(string fullTemplateName);
    }
}
