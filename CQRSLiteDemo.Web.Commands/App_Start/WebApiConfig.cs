using CQRSLiteDemo.Web.Commands.Filters;
using CQRSLiteDemo.Web.Commands.Validation;
using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CQRSLiteDemo.Web.Commands
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services'
            config.Filters.Add(new BadRequestActionFilter());

            FluentValidationModelValidatorProvider.Configure(config, x => x.ValidatorFactory = new StructureMapValidatorFactory(config));

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
