using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Abstractions;
using BusinessLogicInterface;


namespace WebApiTest.FiltersTest
{
    [TestClass]
    public class ExceptionFilterTest
    {
        [TestMethod]
        public void LoginExceptionTest()
        {
            Mock<ISessionLogic> sessionMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            string username = "SomeUsername";
            string password = "SomePassword";
            LoginException exception = new LoginException();
            sessionMock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Throws(exception);
            ModelStateDictionary modelState = new ModelStateDictionary();
            DefaultHttpContext httpContext = new DefaultHttpContext();
            ExceptionContext context = new ExceptionContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());


            ExceptionFilter exceptionFilter = new ExceptionFilter();
            exceptionFilter.OnException(context);

            Assert.AreEqual(httpContext.Response.StatusCode, 403);
            Assert.AreEqual(httpContext.Response.Body, exception.Message);
        }
    }
}
