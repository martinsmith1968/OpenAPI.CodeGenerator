﻿using System;
using OpenAPI.CodeGenerator.Common.Types;

namespace OpenAPI.CodeGenerator.Common.Interfaces
{
    public interface IRenderEngine
    {
        string Name { get; }

        string FileExtension { get; }

        void InitialiseIncludes(ITemplateProvider templateProvider, ILanguage language);

        void RegisterType(Type type);

        string RenderTemplate(string templateText, object parameters);
    }
}
