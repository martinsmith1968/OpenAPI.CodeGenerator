using System.Collections.Generic;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface ITemplateProvider
    {
        TemplateProviderType TemplateProviderType { get; }

        IList<string> GetAvailableLanguages();

        string GetBaseLocation(ILanguage language);
        string GetTemplatePath(IRenderEngine renderEngine, ILanguage language);

        string GetTemplate(IRenderEngine renderEngine, ILanguage language, TemplateItemType templateItemType);

        bool DoesTemplateExist(ILanguage language, string fullTemplateName);
    }
}
