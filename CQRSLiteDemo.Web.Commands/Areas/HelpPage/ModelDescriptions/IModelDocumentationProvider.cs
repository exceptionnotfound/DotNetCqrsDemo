using System;
using System.Reflection;

namespace CQRSLiteDemo.Web.Commands.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}