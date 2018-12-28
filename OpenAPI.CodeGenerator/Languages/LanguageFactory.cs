using System;
using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;
using OpenAPI.CodeGenerator.Language.CSharp;
using OpenAPI.CodeGenerator.Language.CSV;

namespace OpenAPI.CodeGenerator.Languages
{
    public static class LanguageFactory
    {
        public static ILanguage GetLanguage(LanguageType languageType, string languageOptions)
        {
            var language = CreateLanguage(languageType);
            if (language != null)
            {
                var languageOptionsArray = (languageOptions ?? string.Empty).Split(" ".ToCharArray());

                language.ApplyArguments(languageOptionsArray);
            }

            return language;
        }

        private static ILanguage CreateLanguage(LanguageType languageType)
        {
            switch (languageType)
            {
                case LanguageType.csharp:
                    return new CSharpLanguage();

                case LanguageType.CSV:
                    return new CSVLanguage();

                default:
                    throw new ArgumentOutOfRangeException(nameof(languageType), $"Invalid or unsupported {nameof(LanguageType)}: {languageType.ToString()}");
            }
        }
    }
}
