using BusinessLogicInterface;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Filters;

namespace WebApiTest.FiltersTest
{
    [TestClass]
    public class AuthorizationWithParameterFilterTest
    {
        [DataRow(RoleType.Admin)]
        [DataRow(RoleType.Developer)]
        [DataRow(RoleType.Tester)]
        [DataTestMethod]
        public void TestAuthFilterWithValidHeader(RoleType roleType)
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            Mock<ISessionLogic> getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            getRoleMock.Setup(x => x.GetRoleByToken(token)).Returns(roleType);
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(ISessionLogic)))
                .Returns(getRoleMock.Object);
            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);
            httpContextMock.SetupGet(context => context.Request.Headers["token"]).Returns(token);
            ActionContext actionContextMock = new ActionContext(httpContextMock.Object, new Microsoft.AspNetCore.Routing.RouteData(),
                                                                 new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            AuthorizationFilterContext authFilterContext = new AuthorizationFilterContext(actionContextMock, new List<IFilterMetadata>());


            AuthorizationWithParameterFilter authFilter = new AuthorizationWithParameterFilter(roleType);
            authFilter.OnAuthorization(authFilterContext);

            ContentResult response = authFilterContext.Result as ContentResult;
            Assert.IsNull(response);
        }

        [DataRow(RoleType.Admin, "Authentication failed: please log in as an Admin.")]
        [DataRow(RoleType.Developer, "Authentication failed: please log in as a Developer.")]
        [DataRow(RoleType.Tester, "Authentication failed: please log in as a Tester.")]
        [DataTestMethod]
        public void TestAuthFilterWithNoHeader(RoleType roleType, string message)
        {
            Mock<ISessionLogic> getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(ISessionLogic)))
                .Returns(getRoleMock.Object);
            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);
            httpContextMock.SetupGet(context => context.Request.Headers["token"]).Returns((string)null);
            ActionContext actionContextMock = new ActionContext(httpContextMock.Object, new Microsoft.AspNetCore.Routing.RouteData(),
                                                                 new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            AuthorizationFilterContext authFilterContext = new AuthorizationFilterContext(actionContextMock, new List<IFilterMetadata>());


            AuthorizationWithParameterFilter authFilter = new AuthorizationWithParameterFilter(roleType);
            authFilter.OnAuthorization(authFilterContext);

            ContentResult response = authFilterContext.Result as ContentResult;
            Assert.AreEqual(401, response.StatusCode);
            Assert.AreEqual(message, response.Content);
        }
    }
}
