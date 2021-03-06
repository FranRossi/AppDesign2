using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BusinessLogicInterface;
using Domain.DomainUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Filters;

namespace WebApiTest.FiltersTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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

            RoleType[] roles = new[] { roleType };
            AuthorizationWithParameterFilter authFilter = new AuthorizationWithParameterFilter(roles);
            authFilter.OnAuthorization(authFilterContext);

            ContentResult response = authFilterContext.Result as ContentResult;
            Assert.IsNull(response);
        }

        [DataRow(RoleType.Admin, "Please send a valid token.")]
        [DataRow(RoleType.Developer, "Please send a valid token.")]
        [DataRow(RoleType.Tester, "Please send a valid token.")]
        [DataTestMethod]
        public void TestAuthFilterWithNoHeader(RoleType roleType, string message)
        {
            Mock<ISessionLogic> getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            getRoleMock.Setup(x => x.GetRoleByToken(It.IsAny<string>())).Returns(roleType);
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

            RoleType[] roles = new[] { roleType };
            AuthorizationWithParameterFilter authFilter = new AuthorizationWithParameterFilter(roles);
            authFilter.OnAuthorization(authFilterContext);

            ContentResult response = authFilterContext.Result as ContentResult;
            Assert.AreEqual(403, response.StatusCode);
            Assert.AreEqual(message, response.Content);
        }

        [DataRow(RoleType.Developer, RoleType.Admin, "Authentication failed: please log in as: \n- Admin\n")]
        [DataRow(RoleType.Tester, RoleType.Admin, "Authentication failed: please log in as: \n- Admin\n")]
        [DataRow(RoleType.Admin, RoleType.Developer, "Authentication failed: please log in as: \n- Developer\n")]
        [DataRow(RoleType.Tester, RoleType.Developer, "Authentication failed: please log in as: \n- Developer\n")]
        [DataRow(RoleType.Developer, RoleType.Tester, "Authentication failed: please log in as: \n- Tester\n")]
        [DataRow(RoleType.Admin, RoleType.Tester, "Authentication failed: please log in as: \n- Tester\n")]
        [DataTestMethod]
        public void TestAuthFilterWithMismatchingRoles(RoleType actualRoleType, RoleType expectedRoleType, string message)
        {
            Mock<ISessionLogic> getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            getRoleMock.Setup(x => x.GetRoleByToken(It.IsAny<string>())).Returns(actualRoleType);
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(ISessionLogic)))
                .Returns(getRoleMock.Object);
            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);
            httpContextMock.SetupGet(context => context.Request.Headers["token"]).Returns("ohasdhaoistdeh234235");
            ActionContext actionContextMock = new ActionContext(httpContextMock.Object, new Microsoft.AspNetCore.Routing.RouteData(),
                                                                 new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            AuthorizationFilterContext authFilterContext = new AuthorizationFilterContext(actionContextMock, new List<IFilterMetadata>());

            RoleType[] roles = new[] { expectedRoleType };
            AuthorizationWithParameterFilter authFilter = new AuthorizationWithParameterFilter(roles);
            authFilter.OnAuthorization(authFilterContext);

            ContentResult response = authFilterContext.Result as ContentResult;
            Assert.AreEqual(401, response.StatusCode);
            Assert.AreEqual(message, response.Content);
        }

        [DataRow(RoleType.Admin)]
        [DataRow(RoleType.Developer)]
        [DataRow(RoleType.Tester)]
        [DataTestMethod]
        public void TestAuthFilterWithInvalidRole(RoleType roleType)
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

            RoleType[] roles = new[] { RoleType.Invalid };
            AuthorizationWithParameterFilter authFilter = new AuthorizationWithParameterFilter(roles);
            authFilter.OnAuthorization(authFilterContext);

            ContentResult response = authFilterContext.Result as ContentResult;
            Assert.IsNull(response);
        }
    }
}