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
using WebApi.Filters;
using System.Xml;
using System.Diagnostics.CodeAnalysis;
using ExternalReader;
using Utilities.CustomExceptions;
using Utilities.CustomExceptions.DataAccess;
using Utilities.CustomExceptions.FileHandler;
using Utilities.CustomExceptions.WebApi;

namespace WebApiTest.FiltersTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExceptionFilterTest
    {
        [TestMethod]
        public void LoginExceptionTest()
        {
            LoginException exception = new LoginException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void InexistentProjectExceptionTest()
        {
            InexistentProjectException exception = new InexistentProjectException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void InexistentUserExceptionTest()
        {
            InexistentUserException exception = new InexistentUserException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void DataAccessExceptionTest()
        {
            DataAccessException exception = new DataAccessException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void InvalidProjectAssigneeRoleExceptionTest()
        {
            InvalidProjectAssigneeRoleException exception = new InvalidProjectAssigneeRoleException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void ProjectNameIsNotUniqueExceptionTest()
        {
            ProjectNameIsNotUniqueException exception = new ProjectNameIsNotUniqueException();
            TestException(exception, 409);
        }

        [TestMethod]
        public void DomainExceptionTest()
        {
            DomainValidationException exception = new DomainValidationException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void ProjectDoesntBelongToUserExceptionTest()
        {
            UserIsNotAssignedToProjectException exception = new UserIsNotAssignedToProjectException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void BugAlreadyFixedExceptionTest()
        {
            BugAlreadyFixedException exception = new BugAlreadyFixedException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void InexistentBugExceptionTest()
        {
            InexistentBugException exception = new InexistentBugException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void CompanyIsNotRegisteredExceptionTest()
        {
            CompanyIsNotRegisteredException exception = new CompanyIsNotRegisteredException();
            TestException(exception, 403);
        }

        [TestMethod]
        public void XmlExceptionTest()
        {
            XmlException exception = new XmlException();
            TestException(exception, 400);
        }

        [TestMethod]
        public void InvalidCompany2BugFileExceptionTest()
        {
            InvalidCompany2BugFileException exception = new InvalidCompany2BugFileException();
            TestException(exception, 400);
        }

        [TestMethod]
        public void ModelMissingFieldsExceptionTest()
        {
            ModelMissingFieldsException exception = new ModelMissingFieldsException();
            TestException(exception, 400);
        }

        [TestMethod]
        public void ExternalReaderExceptionTest()
        {
            ExternalReaderException exception = new ExternalReaderException();
            TestException(exception, 403);
        }



        private void TestException(Exception exception, int statusCode)
        {
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
            Assert.AreEqual(response.StatusCode, statusCode);
            Assert.AreEqual(response.Content, exception.Message);
        }
    }
}