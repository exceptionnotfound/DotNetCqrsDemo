using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace CQRSLiteDemo.Web.Commands.Filters
{
    //Stolen from http://sergeyakopov.com/restful-validation-with-asp-net-web-api-and-fluentvalidation/
    public class ValidationErrorWrapper
    {
        private const string ErrorMessage = "The request is invalid.";

        private const string MissingPropertyError = "Undefined error.";

        public ValidationErrorWrapper(ModelStateDictionary modelState)
        {
            Message = ErrorMessage;
            SerializeModelState(modelState);
        }

        public ValidationErrorWrapper(string message, ModelStateDictionary modelState)
        {
            Message = message;
            SerializeModelState(modelState);
        }

        public string Message { get; private set; }

        public IDictionary<string, IEnumerable<string>> Errors { get; private set; }

        private void SerializeModelState(ModelStateDictionary modelState)
        {
            Errors = new Dictionary<string, IEnumerable<string>>();

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;

                var errors = keyModelStatePair.Value.Errors;

                if (errors != null && errors.Count > 0)
                {
                    IEnumerable<string> errorMessages = errors.Select(
                        error => string.IsNullOrEmpty(error.ErrorMessage)
                                     ? MissingPropertyError
                                     : error.ErrorMessage).ToArray();

                    Errors.Add(key, errorMessages);
                }
            }
        }
    }
}