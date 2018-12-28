using System.IO;
using OpenAPI.CodeGenerator.Common.Languages;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Language.CSharp
{
    public class CSharpLanguage : BaseLanguage
    {
        private CSharpOptions _options;

        public override string FileExtension => "cs";

        public override void ApplyArguments(string[] args)
        {
            _options = CSharpOptions.Create(args);
        }

        public override string BuildOutputFileName(string itemName, TemplateItemType templateItemType)
        {
            switch(_options.OutputType)
            {
                case CSharpOptions.CSharpOutputType.SingleFile:
                    return BuildSingleOutputFileName(itemName, templateItemType);

                case CSharpOptions.CSharpOutputType.FilePerClass:
                    return BuildMultipleOutputFileName(itemName, templateItemType);

                default:
                    return null;
            }
        }

        private string BuildMultipleOutputFileName(string itemName, TemplateItemType templateItemType)
        {
            var subFolder = Pluralize(templateItemType.ToString());

            switch (templateItemType)
            {
                case TemplateItemType.Controller:
                    return Path.Combine(subFolder,  Path.ChangeExtension($"{itemName}{templateItemType}", FileExtension));

                case TemplateItemType.Interface:
                    return Path.Combine(subFolder, Path.ChangeExtension($"{templateItemType.ToString().Substring(0, 1)}{itemName}", FileExtension));

                case TemplateItemType.Model:
                    return Path.Combine(subFolder, Path.ChangeExtension($"{itemName}", FileExtension));

                default:
                    return null;
            }
        }

        private string BuildSingleOutputFileName(string itemName, TemplateItemType templateItemType)
        {
            switch (templateItemType)
            {
                case TemplateItemType.Definition:
                    return Path.GetFileNameWithoutExtension(itemName) == itemName
                        ? Path.ChangeExtension(itemName, FileExtension)
                        : itemName;

                default:
                    return null;
            }
        }
    }
}
