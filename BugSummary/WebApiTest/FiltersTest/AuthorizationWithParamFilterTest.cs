using System;
using System.Collections.Generic;
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
    public class AuthorizationWithParameterFilterTest
    {
        [DataRow(RoleType.Admin)]
        [DataRow(RoleType.Developer)]
        [DataRow(RoleType.Tester)]
        [DataTestMethod]
        public void TestAuthFilterWithValidHeader(RoleType roleType)
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            var getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            getRoleMock.Setup(x => x.GetRoleByToken(token)).Returns(roleType);
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(ISessionLogic)))
                .Returns(getRoleMock.Object);
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);
            httpContextMock.SetupGet(context => context.Request.Headers["token"]).Returns(token);
            var actionContextMock = new ActionContext(httpContextMock.Object, new RouteData(),
                new ActionDescriptor());
            var authFilterContext = new AuthorizationFilterContext(actionContextMock, new List<IFilterMetadata>());


            var authFilter = new AuthorizationWithParameterFilter(roleType);
            authFilter.OnAuthorization(authFilterContext);

            var response = authFilterContext.Result as ContentResult;
            Assert.IsNull(response);
        }

        [DataRow(RoleType.Admin, "Please send a valid token.")]
        [DataRow(RoleType.Developer, "Please send a valid token.")]
        [DataRow(RoleType.Tester, "Please send a valid token.")]
        [DataTestMethod]
        public void TestAuthFilterWithNoHeader(RoleType roleType, string message)
        {
            var getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            getRoleMock.Setup(x => x.GetRoleByToken(It.IsAny<string>())).Returns(roleType);
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(ISessionLogic)))
                .Returns(getRoleMock.Object);
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);
            httpContextMock.SetupGet(context => context.Request.Headers["token"]).Returns((string) null);
            var actionContextMock = new ActionContext(httpContextMock.Object, new RouteData(),
                new ActionDescriptor());
            var authFilterContext = new AuthorizationFilterContext(actionContextMock, new List<IFilterMetadata>());


            var authFilter = new AuthorizationWithParameterFilter(roleType);
            authFilter.OnAuthorization(authFilterContext);

            var response = authFilterContext.Result as ContentResult;
            Assert.AreEqual(403, response.StatusCode);
            Assert.AreEqual(message, response.Content);
        }

        [DataRow(RoleType.Developer, RoleType.Admin, "Authentication failed: please log in as Admin")]
        [DataRow(RoleType.Tester, RoleType.Admin, "Authentication failed: please log in as Admin")]
        [DataRow(RoleType.Admin, RoleType.Developer, "Authentication failed: please log in as Developer")]
        [DataRow(RoleType.Tester, RoleType.Developer, "Authentication failed: please log in as Developer")]
        [DataRow(RoleType.Developer, RoleType.Tester, "Authentication failed: please log in as Tester")]
        [DataRow(RoleType.Admin, RoleType.Tester, "Authentication failed: please log in as Tester")]
        [DataTestMethod]
        public void TestAuthFilterWithMismatchingRoles(RoleType actualRoleType, RoleType expectedRoleType,
            string message)
        {
            var getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            getRoleMock.Setup(x => x.GetRoleByToken(It.IsAny<string>())).Returns(actualRoleType);
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(ISessionLogic)))
                .Returns(getRoleMock.Object);
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);
            httpContextMock.SetupGet(context => context.Request.Headers["token"]).Returns("ohasdhaoistdeh234235");
            var actionContextMock = new ActionContext(httpContextMock.Object, new RouteData(),
                new ActionDescriptor());
            var authFilterContext = new AuthorizationFilterContext(actionContextMock, new List<IFilterMetadata>());


            var authFilter = new AuthorizationWithParameterFilter(expectedRoleType);
            authFilter.OnAuthorization(authFilterContext);

            var response = authFilterContext.Result as ContentResult;
            Assert.AreEqual(401, response.StatusCode);
            Assert.AreEqual(message, response.Content);
        }

        [DataRow(RoleType.Admin)]
        [DataRow(RoleType.Developer)]
        [DataRow(RoleType.Tester)]
        [DataTestMethod]
        public void TestAuthFilterWithInvalidRole(RoleType roleType)
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            var getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            getRoleMock.Setup(x => x.GetRoleByToken(token)).Returns(roleType);
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(ISessionLogic)))
                .Returns(getRoleMock.Object);
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);
            httpContextMock.SetupGet(context => context.Request.Headers["token"]).Returns(token);
            var actionContextMock = new ActionContext(httpContextMock.Object, new RouteData(),
                new ActionDescriptor());
            var authFilterContext = new AuthorizationFilterContext(actionContextMock, new List<IFilterMetadata>());


            var authFilter = new AuthorizationWithParameterFilter(RoleType.Invalid);
            authFilter.OnAuthorization(authFilterContext);

            var response = authFilterContext.Result as ContentResult;
            Assert.IsNull(response);
        }
    }
}