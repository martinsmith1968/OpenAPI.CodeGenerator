using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface ILanguageFactory
    {
        ILanguage GetLanguageAndConfigure(LanguageType languageType, string languageOptions);

        ILanguage GetLanguage(LanguageType languageType);
    }
}
