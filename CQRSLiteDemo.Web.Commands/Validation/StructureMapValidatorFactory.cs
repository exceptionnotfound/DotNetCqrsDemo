using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CQRSLiteDemo.Web.Commands.Validation
{
    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        private readonly HttpConfiguration _configuration;

        public StructureMapValidatorFactory(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _configuration.DependencyResolver.GetService(validatorType) as IValidator;
        }
    }
}