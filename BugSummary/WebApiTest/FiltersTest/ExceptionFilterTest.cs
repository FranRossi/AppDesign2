using System;
using System.Collections.Generic;
using Domain.DomainUtilities.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var exception = new LoginException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void InexistentProjectExceptionTest()
        {
            var exception = new InexistentProjectException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void InexistentUserExceptionTest()
        {
            var exception = new InexistentUserException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void InvalidProjectAssigneeRoleExceptionTest()
        {
            var exception = new InvalidProjectAssigneeRoleException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void ProjectNameIsNotUniqueExceptionTest()
        {
            var exception = new ProjectNameIsNotUniqueException();
            TestException(exception, 409);
        }

        [TestMethod]
        public void DomainExceptionTest()
        {
            var exception = new DomainValidationException();
            TestException(exception, 403);
        }

        private void TestException(Exception exception, int statusCode)
        {
            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var context = new ExceptionContext(
                new ActionContext(httpContext,
                    new RouteData(),
                    new ActionDescriptor(),
                    modelState),
                new List<IFilterMetadata>());


            var exceptionFilter = new ExceptionFilter();
            context.Exception = exception;
            exceptionFilter.OnException(context);

            var response = context.Result as ContentResult;
            Assert.AreEqual(response.StatusCode, statusCode);
            Assert.AreEqual(response.Content, exception.Message);
        }
    }
}