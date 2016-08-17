using System;
using System.Reflection;

namespace CQRSLiteDemo.Web.Queries.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}