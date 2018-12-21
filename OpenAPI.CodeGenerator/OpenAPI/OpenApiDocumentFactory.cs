using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace OpenAPI.CodeGenerator.OpenAPI
{
    public class OpenApiDocumentFactory
    {
        public static OpenApiDocument ReadDocumentFromFile(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                return ReadDocumentFromStream(fileStream);
            }
        }

        public static OpenApiDocument ReadDocumentFromStream(Stream stream)
        {
            var settings = new OpenApiReaderSettings()
            {
            };

            return new OpenApiStreamReader(settings)
                    .Read(stream, out var diagnostic)
                ;
        }
    }
}
