using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OpenAPI.CodeGenerator.Common.Languages;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Language.JSON
{
    public class JSONLanguage : BaseLanguage
    {
        public static JsonSerializerSettings Settings = new JsonSerializerSettings();

        public override string FileExtension => "csv";

        private string _definitionFileName;

        public override string BuildOutputFileName(string itemName, TemplateItemType templateItemType)
        {
            if (templateItemType == TemplateItemType.Definition)
            {
                _definitionFileName = itemName;
            }

            return _definitionFileName;
        }

        public override void ApplyArguments(string[] args)
        {
            Settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter>()
                {
                    new StringEnumConverter
                    {
                        NamingStrategy = new DefaultNamingStrategy()
                    }
                }
            };
        }
    }
}
