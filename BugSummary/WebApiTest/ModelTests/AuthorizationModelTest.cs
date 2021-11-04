using System.Diagnostics.CodeAnalysis;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AuthorizationModelTest
    {
        [TestMethod]
        public void CreateModel()
        {
            AuthorizationModel model = new AuthorizationModel
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX",
                Role = RoleType.Admin
            };
            Assert.IsNotNull(model);
        }

    }
}