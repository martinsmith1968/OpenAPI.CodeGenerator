using System;
using System.Collections.Generic;
using System.Linq;
using OpenAPI.CodeGenerator.Common.Extensions;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Factories
{
    public class LanguageFactory : ILanguageFactory
    {
        public IEnumerable<ILanguage> Languages { get; }

        public LanguageFactory(IEnumerable<ILanguage> languages)
        {
            Languages = languages;
        }

        public ILanguage GetLanguageAndConfigure(string languageName, string languageOptions)
        {
            var language = GetLanguageByName(languageName);

            var languageOptionsArray = (languageOptions ?? string.Empty).Split(" ".ToCharArray());
            language.ApplyArguments(languageOptionsArray);

            return language;
        }

        public ILanguage GetLanguageByName(string languageName)
        {
            var language = Languages
                    .SingleOrDefault(c => string.Equals(c.Name, languageName, StringComparison.OrdinalIgnoreCase))
                ;
            if (language == null)
                throw new ArgumentOutOfRangeException(nameof(languageName), $"Invalid or unsupported {typeof(ILanguage).GetNonInterfaceName()}: {languageName}");

            return language;
        }
    }
}
