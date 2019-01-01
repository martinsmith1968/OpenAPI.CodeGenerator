using System.Collections.Generic;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface ITemplateProvider
    {
        TemplateProviderType TemplateProviderType { get; }

        string BaseLocation { get; }

        IList<string> GetAvailableLanguages();

        string GetTemplatePath(IRenderEngine renderEngine, ILanguage language);

        string GetTemplate(IRenderEngine renderEngine, ILanguage language, TemplateItemType templateItemType);

        bool DoesTemplateExist(string fullTemplateName);
    }
}
