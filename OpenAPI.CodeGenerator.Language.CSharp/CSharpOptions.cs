namespace OpenAPI.CodeGenerator.Language.CSharp
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
            // TODO
            var options = new CSharpOptions()
            {
                OutputType = CSharpOutputType.SingleFile
            };

            return options;
        }
    }
}
