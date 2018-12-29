using System;
using Autofac;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Interfaces;

namespace OpenAPI.CodeGenerator.Languages
{
    public class LanguageFactory : ILanguageFactory
    {
        public ILanguage GetLanguageAndConfigure(LanguageType languageType, string languageOptions)
        {
            var language = GetLanguage(languageType);
            if (language != null)
            {
                var languageOptionsArray = (languageOptions ?? string.Empty).Split(" ".ToCharArray());

                language.ApplyArguments(languageOptionsArray);
            }

            return language;
        }

        public ILanguage GetLanguage(LanguageType languageType)
        {
            var language = Program.Container.ResolveOptionalKeyed<ILanguage>(languageType);
            if (language == null)
                throw new ArgumentOutOfRangeException(nameof(languageType), $"Invalid or unsupported {nameof(LanguageType)}: {languageType.ToString()}");

            return language;
        }
    }
}
