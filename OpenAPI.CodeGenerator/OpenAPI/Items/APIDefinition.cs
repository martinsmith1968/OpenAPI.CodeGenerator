using System.Collections.Generic;
using System.IO;
using Microsoft.OpenApi.Models;
using OpenAPI.CodeGenerator.OpenAPI.Extensions;

namespace OpenAPI.CodeGenerator.OpenAPI.Items
{
    public class APIDefinition
    {
        public FileInfo FileInfo { get; private set; }

        public OpenApiDocument OpenApiDocument { get; private set; }

        public bool IsValid => FileInfo != null && FileInfo.Exists;

        public string GroupName { get; private set; }

        public string FileName => FileInfo?.Name;

        public string Name => Path.GetFileNameWithoutExtension(FileName);

        public string Host => OpenApiDocument.GetHost();

        public string BasePath => OpenApiDocument.GetBasePath();

        public IList<APIController> Controllers => OpenApiDocument.GetControllers();

        public static APIDefinition Create(string fileName, OpenApiDocument document, string groupName = null)
        {
            return new APIDefinition
            {
                GroupName       = groupName,
                FileInfo        = new FileInfo(fileName),
                OpenApiDocument = document
            };
        }
    }
}
