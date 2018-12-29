using OpenAPI.CodeGenerator.Common.Interfaces;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Interfaces
{
    public interface IOutputWriterFactory
    {
        IOutputWriter GetOutputWriter(OutputTargetType outputTargetType);
    }
}
