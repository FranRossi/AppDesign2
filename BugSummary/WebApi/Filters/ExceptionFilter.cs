﻿using Microsoft.AspNetCore.Mvc;
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

            context.Result = new ContentResult()
            {
                StatusCode = statusCode,
                Content = exceptionMessage
            };
        }
    }
}