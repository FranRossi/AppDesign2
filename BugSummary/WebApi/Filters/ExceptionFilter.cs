﻿using System;
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
            string exceptionMessage = context.Exception.Message;

            if (context.Exception is ProjectNameIsNotUniqueException || context.Exception is UsernameIsNotUniqueException)
            {
                statusCode = 409;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is LoginException || context.Exception is DomainValidationException
                || context.Exception is DataAccessException || context.Exception is CompanyIsNotRegisteredException)
            {
                statusCode = 403;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is ModelMissingFieldsException || context.Exception is XmlException
                || context.Exception is InvalidCompany2BugFileException)
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