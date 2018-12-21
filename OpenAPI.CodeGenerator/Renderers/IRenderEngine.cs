namespace OpenAPI.CodeGenerator.Renderers
{
    public interface IRenderEngine
    {
        string RenderTemplate(string templateText, object parameters);
    }
}
