namespace OpenAPI.CodeGenerator.Common.Extensions
{
    public static class StringExtensions
    {
        public static string UpperCaseFirstLetter(this string text)
        {
            return string.IsNullOrEmpty(text)
                ? text
                : string.Concat(text.Substring(0, 1).ToUpper(), text.Substring(1));
        }

        public static string UpperCaseFirstLetterLowercaseRest(this string text)
        {
            return string.IsNullOrEmpty(text)
                ? text
                : string.Concat(text.Substring(0, 1).ToUpper(), text.Substring(1).ToLower());
        }
    }
}
