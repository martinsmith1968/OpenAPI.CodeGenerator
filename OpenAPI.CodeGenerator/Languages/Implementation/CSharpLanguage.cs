using System.IO;
using OpenAPI.CodeGenerator.Common.Languages;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Languages.Implementation
{
    public class CSharpOptions
    {
        public enum CSharpOutputType
        {
            SingleFile,
            FilePerClass
        }

        public CSharpOutputType OutputType { get; private set; }

        public static CSharpOptions Create(string[] optionsArgs)
        {
            var options = new CSharpOptions()
            {
                OutputType = CSharpOutputType.SingleFile
            };

            return options;
        }
    }

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
            return BuildSingleOtputFileName(itemName, templateItemType);
            /*
            switch (outputType)
            {
                case OutputType.SingleFile:
                    return BuildSingleOtputFileName(itemName, templateItemType);

                case OutputType.MultipleFiles:
                    return BuildMutipleOutputFileName(itemName, templateItemType);

                default:
                    return null;
            }
            */
        }

        private string BuildMutipleOutputFileName(string itemName, TemplateItemType templateItemType)
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

        private string BuildSingleOtputFileName(string itemName, TemplateItemType templateItemType)
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
