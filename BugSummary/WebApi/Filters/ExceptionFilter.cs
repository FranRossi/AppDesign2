using Domain.DomainUtilities.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.CustomExceptions;

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
            else if (context.Exception is ProjectNameIsNotUniqueException)
            {
                statusCode = 409;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is InexistentProjectException)
            {
                statusCode = 403;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is InexistentUserException)
            {
                statusCode = 403;
                exceptionMessage = context.Exception.Message;
            }
            else if (context.Exception is InvalidProjectAssigneeRoleException)
            {
                statusCode = 403;
                exceptionMessage = context.Exception.Message;
            }

            context.Result = new ContentResult()
            {
                StatusCode = statusCode,
                Content = exceptionMessage
            };
        }
    }
}