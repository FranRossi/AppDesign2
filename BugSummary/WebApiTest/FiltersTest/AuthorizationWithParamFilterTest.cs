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

namespace WebApiTest.FiltersTest
{
    public class AuthorizationWithParameterFilterTest
    {
        [TestMethod]
        public void TestAuthFilterWithValidHeader()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            var getRoleMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            getRoleMock.Setup(x => x.GetRoleByToken(token)).Returns(RoleType.Admin);
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(ISessionLogic)))
                .Returns(getRoleMock.Object);
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);
            httpContextMock.SetupGet(context => context.Request.Headers["token"]).Returns(token);
            ActionContext actionContextMock = new ActionContext(httpContextMock.Object, new Microsoft.AspNetCore.Routing.RouteData(),
                                                                 new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            AuthorizationFilterContext authFilterContext = new AuthorizationFilterContext(actionContextMock, new List<IFilterMetadata>());


            AuthorizationWithParameterFilter authFilter = new AuthorizationWithParameterFilter("Admin");
            authFilter.OnAuthorization(authFilterContext);

            ContentResult response = authFilterContext.Result as ContentResult;
            Assert.IsNull(response);
        }
    }
}
