using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilities.Comparers;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class ProjectControllerTest
    {
        [TestMethod]
        public void ProjectToEntityTest()
        {
            ProjectModel projectModel = new ProjectModel
            {
                Name = "New Project 2022"
            };
            Project ExpectedProject = new Project
            {
                Name = "New Project 2022"
            };

            Project result = projectModel.ToEntity(ExpectedProject);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(ExpectedProject, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
