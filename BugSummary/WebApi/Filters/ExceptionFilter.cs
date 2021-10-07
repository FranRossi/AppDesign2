using System;
using Domain.DomainUtilities.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utilities.CustomExceptions;
using CustomExceptions;
using System.Xml;

namespace WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode = 500;
            string exceptionMessage = "";

            if (context.Exception is LoginException)
            {
                statusCode = 403;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is ProjectNameIsNotUniqueException || context.Exception is UsernameIsNotUniqueException)
            {
                statusCode = 409;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is DomainValidationException || context.Exception is DataAccessException)
            {
                statusCode = 403;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is CompanyIsNotRegisteredException)
            {
                statusCode = 403;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is XmlException)
            {
                statusCode = 400;
                exceptionMessage = context.Exception.Message;
            }

            context.Result = new ContentResult
            {
                StatusCode = statusCode,
                Content = exceptionMessage
            };
        }
    }
}