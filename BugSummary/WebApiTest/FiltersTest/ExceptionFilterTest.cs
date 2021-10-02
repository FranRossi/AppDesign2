﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Abstractions;
using BusinessLogicInterface;
using Utilities.CustomExceptions;
using WebApi.Filters;

namespace WebApiTest.FiltersTest
{
    [TestClass]
    public class ExceptionFilterTest
    {
        [TestMethod]
        public void LoginExceptionTest()
        {
            LoginException exception = new LoginException();
            ModelStateDictionary modelState = new ModelStateDictionary();
            DefaultHttpContext httpContext = new DefaultHttpContext();
            ExceptionContext context = new ExceptionContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());


            ExceptionFilter exceptionFilter = new ExceptionFilter();
            context.Exception = exception;
            exceptionFilter.OnException(context);

            ContentResult response = context.Result as ContentResult;
            Assert.AreEqual(response.StatusCode, 403);
            Assert.AreEqual(response.Content, exception.Message);
        }
    }
}
