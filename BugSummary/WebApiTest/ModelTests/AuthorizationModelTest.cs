using System.Diagnostics.CodeAnalysis;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
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
        
        [TestMethod]
        public void BugToModel()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            RoleType role = RoleType.Admin;
            AuthorizationModel expectedModel = new AuthorizationModel
            {
                Token = token,
                Role = role
            };
            
            AuthorizationModel newModel = AuthorizationModel.ToModel(token, role);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, newModel);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
        
    }
    
    
}