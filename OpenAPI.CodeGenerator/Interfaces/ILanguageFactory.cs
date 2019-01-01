using OpenAPI.CodeGenerator.Common.Interfaces;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface ILanguageFactory
    {
        ILanguage GetLanguageAndConfigure(string languageName, string languageOptions);

        ILanguage GetLanguageByName(string languageName);
    }
}
