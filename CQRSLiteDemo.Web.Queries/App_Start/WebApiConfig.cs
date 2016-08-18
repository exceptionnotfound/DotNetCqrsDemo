using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CQRSLiteDemo.Web.Queries
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
